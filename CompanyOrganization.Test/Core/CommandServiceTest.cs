using System;
using System.Collections.Generic;
using System.Linq;
using CompanyEmployeesBalancing.Domain;
using CompanyEmployeesBalancing.Domain.BusinessObjects;
using CompanyEmployeesBalancing.Domain.Enumeration;
using CompanyEmployeesBalancing.Resources;
using CompanyEmployeesBalancing.Services.Implementation;
using CompanyEmployeesBalancing.Services.Storage;
using CompanyEmployeesBalancing.Test.Configuration;
using CompanyEmployeesBalancing.Test.Faker;
using Faker;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Core
{
    [TestClass]
    public class CommandServiceTest
    {
        private readonly CommandService _commandService;
        private readonly TeamService _teamService;

        public CommandServiceTest()
        {
            _commandService = DependencyResolver.Get<CommandService>();
            _teamService = DependencyResolver.Get<TeamService>();
        }

        [TestMethod]
        public void TestGetCommandWithInvalidCommand()
        {

            var exception = Assert.ThrowsException<Exception>(() => _commandService.GetCommand("invalid command"));
            Assert.AreEqual(Messages.InvalidCommand, exception.Message);
        }

        [TestMethod]
        public void TestGetCommandLoadCommandWithoutArguments()
        {
            var exception = Assert.ThrowsException<Exception>(() => _commandService.GetCommand("load"));
            Assert.AreEqual(Messages.InvalidFilesToLoad, exception.Message);
        }

        [TestMethod]
        public void TestGetCommandLoadCommandWithOneFile()
        {
            var exception = Assert.ThrowsException<Exception>(() => _commandService.GetCommand("load team.csv"));
            Assert.AreEqual(Messages.InvalidFilesToLoad, exception.Message);
        }

        [TestMethod]
        public void TestGetCommandPromoteWithInvalidArgument()
        {
            var exception = Assert.ThrowsException<Exception>(() => _commandService.GetCommand("promote abc"));
            Assert.AreEqual(Messages.InvalidValue, exception.Message);
        }

        [TestMethod]
        public void TestGetCommandPromoteWithValidArgument()
        {
            var command = _commandService.GetCommand("promote 3");
            Assert.AreEqual("3", command.Arguments.FirstOrDefault());
        }

        [TestMethod]
        public void TestGetCommandAllocateWithArguments()
        {
            var command = _commandService.GetCommand("allocate abc def");
            Assert.AreEqual(CommandEnum.Allocate, command.Command);
            Assert.AreEqual(0, command.Arguments.Count);
        }

        [TestMethod]
        public void TestGetCommandBalanceWithArguments()
        {
            var command = _commandService.GetCommand("balance abc def");
            Assert.AreEqual(CommandEnum.Balance, command.Command);
            Assert.AreEqual(0, command.Arguments.Count);
        }

        #region Success GetCommand
        [TestMethod]
        public void TestGetCommandLoadCommandWithValidFilesExtension()
        {
            var command = _commandService.GetCommand("load team.csv employees.csv");
            Assert.AreEqual(CommandEnum.Load, command.Command);
            Assert.AreEqual(2, command.Arguments.Count);
        }

        [TestMethod]
        public void TestGetCommandAllocateCommandWithValidFilesExtension()
        {
            var command = _commandService.GetCommand("allocate team.csv employees.csv");
            Assert.AreEqual(CommandEnum.Allocate, command.Command);
            Assert.AreEqual(0, command.Arguments.Count);
        }


        [TestMethod]
        public void TestGetCommandPromoteCommandWithValidFilesExtension()
        {
            var command = _commandService.GetCommand("promote 3");
            Assert.AreEqual(CommandEnum.Promote, command.Command);
            Assert.AreEqual(1, command.Arguments.Count);
        }

        [TestMethod]
        public void TestGetCommandBalanceCommandWithValidFilesExtension()
        {
            var command = _commandService.GetCommand("balance");
            Assert.AreEqual(CommandEnum.Balance, command.Command);
            Assert.AreEqual(0, command.Arguments.Count);
        }

        #endregion

        #region Execute Command

        [TestMethod]
        public void TestExecuteCommandLoad()
        {
            var arguments = new List<string>();
            arguments.Add("team.csv");
            arguments.Add("employees.csv");
            var command = Builder<CommandDto>
                .CreateNew()
                .With(c => c.Command = CommandEnum.Load)
                .With(c => c.Arguments = arguments)
                .Build();

            _commandService.ExecuteCommand(command);
            Assert.AreEqual(3, CompanyLocalStorage.GetInstance.GetTeams().Count);
            Assert.AreEqual(7, CompanyLocalStorage.GetInstance.GetEmployees().Count);
        }

        [TestMethod]
        public void TestExecuteCommandAllocate()
        {
            CompanyLocalStorage.GetInstance.UpdateTeams(TeamFaker.GetCompleteTeams());
            CompanyLocalStorage.GetInstance.UpdateEmployees(EmployeeFaker.GetEmployees());
            var command = Builder<CommandDto>
                .CreateNew()
                .With(c => c.Command = CommandEnum.Allocate)
                .Build();

            _commandService.ExecuteCommand(command);
            Assert.AreEqual(5, CompanyLocalStorage.GetInstance.GetCompany().Teams.Count);
            Assert.AreEqual(20, CompanyLocalStorage.GetInstance.GetCompany().Teams.Sum(team => team.Employees.Count));
        }
        [TestMethod]
        public void TestExecuteCommandPromote()
        {
            CurrentYear.GetInstance.UpdateYear(DateTime.Now.Year);
            var nameEmployeePromoted = "Employee promoted";
            var employees = Builder<Employee>.CreateListOfSize(5).All()
                .With(employee => employee.ProgressionLevel = 3)
                .With(employee => employee.Name = NameFaker.Name())
                .With(employee => employee.BirthYear = 1994)
                .With(employee => employee.AdmissionYear = 2018)
                .With(employee => employee.LastProgressionYear = 2018)
                .Build();

            employees.Add(Builder<Employee>.CreateNew()
                .With(employee => employee.ProgressionLevel = 2)
                .With(employee => employee.Name = nameEmployeePromoted)
                .With(employee => employee.BirthYear = 1970)
                .With(employee => employee.AdmissionYear = 2010)
                .With(employee => employee.LastProgressionYear = 2012).Build());

            CompanyLocalStorage.GetInstance.UpdateEmployees(employees);
            var arguments = new List<string>();
            arguments.Add("1");
            var command = Builder<CommandDto>
                .CreateNew()
                .With(c => c.Command = CommandEnum.Promote)
                .With(c => c.Arguments = arguments)
                .Build();
            _commandService.ExecuteCommand(command);

            Assert.AreEqual(3, employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted)).ProgressionLevel);
            Assert.AreEqual(DateTime.Now.AddYears(1).Year, CurrentYear.GetInstance.Year);


        }
        [TestMethod]
        public void TestExecuteCommandBalance()
        {
            var company = new Company();
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            CompanyLocalStorage.GetInstance.UpdateCompany(company);

            var command = Builder<CommandDto>
                .CreateNew()
                .With(c => c.Command = CommandEnum.Balance)
                .Build();
            _commandService.ExecuteCommand(command);
            Assert.AreEqual(0, company.Teams.Where(team => _teamService.GetExtraMaturity(team) > 0).ToList().Count);
        }
        
        #endregion

        private Team GetTeamWithNoExtraMaturity()
        {
            var employees = Builder<Employee>.CreateListOfSize(5).All()
                .With(employee => employee.ProgressionLevel = 3)
                .With(employee => employee.Name = NameFaker.Name())
                .With(employee => employee.BirthYear = 1994)
                .With(employee => employee.AdmissionYear = 2018)
                .With(employee => employee.LastProgressionYear = 2018)
                .Build();

            var team = Builder<Team>.CreateNew()
                .With(newTeam => newTeam.MinimunMaturity = 15)
                .With(newTeam => newTeam.Name = CompanyFaker.Name())
                .With(newTeam => newTeam.Employees = employees)
                .Build();

            return team;
        }
    }
}
