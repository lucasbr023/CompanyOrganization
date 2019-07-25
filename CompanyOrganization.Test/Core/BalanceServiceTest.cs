using CompanyOrganization;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Implementation;
using CompanyOrganization.Storage;
using CompanyOrganization.Test;
using Faker;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployeesBalancing.Test.Core
{
    [TestClass]
    public class BalanceServiceTest
    {
        private readonly BalanceService _balanceService;
        private readonly TeamService _teamService;
        public BalanceServiceTest()
        {
            _balanceService = DependencyResolver.Get<BalanceService>();
            _teamService = DependencyResolver.Get<TeamService>();
        }

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

        private Team GetTeamWithLessExtraMaturity()
        {
            var employees = Builder<Employee>.CreateListOfSize(4).All()
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

        [TestMethod]
        public void TestBalanceWithoutTeam()
        {
            var company = new Company();
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
            _balanceService.Balance();
            Assert.AreEqual(0, company.Teams.Count);
        }

        [TestMethod]
        public void TestBalanceWithTeamsNoExtraMaturity()
        {
            var company = new Company();
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
            _balanceService.Balance();
            Assert.AreEqual(0, company.Teams.Where(team => _teamService.GetExtraMaturity(team) > 0).ToList().Count);
        }
        

        [TestMethod]
        public void TestBalanceTransferOneEmployeeToTeamMinimumExtraMaturity()
        {
            var company = new Company();
            var team1 = CreateTeamWithNoExtraMaturity();
            var team2 = CreateTeamWithFiveExtraMaturity();
            var team3 = CreateTeamWithTenExtraMaturity();
            
            company.Teams.Add(team1);
            company.Teams.Add(team2);
            company.Teams.Add(team3);
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
            _balanceService.Balance();
            Assert.AreEqual(3, company.Teams.Where(team => _teamService.GetExtraMaturity(team) == 5).ToList().Count);

        }

        private static Team CreateTeamWithTenExtraMaturity()
        {
            var employeesTeam3 = Builder<Employee>.CreateListOfSize(5).All()
                                 .With(employee => employee.ProgressionLevel = 5)
                                 .With(employee => employee.Name = NameFaker.Name())
                                 .With(employee => employee.BirthYear = 1994)
                                 .With(employee => employee.AdmissionYear = 2018)
                                 .With(employee => employee.LastProgressionYear = 2018)
                                 .Build();
            var team3 = Builder<Team>.CreateNew()
              .With(newTeam => newTeam.MinimunMaturity = 15)
              .With(newTeam => newTeam.Name = CompanyFaker.Name())
              .With(newTeam => newTeam.Employees = employeesTeam3)
              .Build();
            return team3;
        }

        private Team CreateTeamWithFiveExtraMaturity()
        {
            var employeesTeam2 = Builder<Employee>.CreateListOfSize(5).All()
                                 .With(employee => employee.ProgressionLevel = 4)
                                 .With(employee => employee.Name = NameFaker.Name())
                                 .With(employee => employee.BirthYear = 1994)
                                 .With(employee => employee.AdmissionYear = 2018)
                                 .With(employee => employee.LastProgressionYear = 2018)
                                 .Build();
            var team2 = Builder<Team>.CreateNew()
              .With(newTeam => newTeam.MinimunMaturity = 15)
              .With(newTeam => newTeam.Name = CompanyFaker.Name())
              .With(newTeam => newTeam.Employees = employeesTeam2)
              .Build();
            return team2;
        }

        private Team CreateTeamWithNoExtraMaturity()
        {
            var employeesTeam1 = Builder<Employee>.CreateListOfSize(5).All()
                                 .With(employee => employee.ProgressionLevel = 3)
                                 .With(employee => employee.Name = NameFaker.Name())
                                 .With(employee => employee.BirthYear = 1994)
                                 .With(employee => employee.AdmissionYear = 2018)
                                 .With(employee => employee.LastProgressionYear = 2018)
                                 .Build();

            var team1 = Builder<Team>.CreateNew()
                .With(newTeam => newTeam.MinimunMaturity = 15)
                .With(newTeam => newTeam.Name = CompanyFaker.Name())
                .With(newTeam => newTeam.Employees = employeesTeam1)
                .Build();
            return team1;
        }

        [TestMethod]
        public void TestBalanceWithTeamLessThanMinimumMaturity()
        {
            var company = new Company();
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithNoExtraMaturity());
            company.Teams.Add(GetTeamWithLessExtraMaturity());

            CompanyLocalStorage.GetInstance.UpdateCompany(company);

            var exception = Assert.ThrowsException<Exception>(() => _balanceService.Balance());
            Assert.AreEqual(Messages.TeamsHaventEnoughMaturity, exception.Message);
        }


      
        [TestMethod]
        public void TestBalanceWithMultiplesBalancesBetweenTeams()
        {
            var company = new Company();
            Team team1 = CreateTeamFourExtraMaturity();

            var employeesTeam2 = Builder<Employee>.CreateListOfSize(10).All()
                     .With(employee => employee.ProgressionLevel = 1)
                     .With(employee => employee.Name = NameFaker.Name())
                     .With(employee => employee.BirthYear = 1994)
                     .With(employee => employee.AdmissionYear = 2018)
                     .With(employee => employee.LastProgressionYear = 2018)
                     .Build();
            var team2 = Builder<Team>.CreateNew()
              .With(newTeam => newTeam.MinimunMaturity = 5)
              .With(newTeam => newTeam.Name = CompanyFaker.Name())
              .With(newTeam => newTeam.Employees = employeesTeam2)
              .Build();

            Team team3 = CreateTeamWithDiferentsEmployeesMaturity();

            company.Teams.Add(team1);
            company.Teams.Add(team2);
            company.Teams.Add(team3);
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
            _balanceService.Balance();
            Assert.AreEqual(2, company.Teams.Where(team => _teamService.GetExtraMaturity(team) == 6).ToList().Count);
            Assert.AreEqual(1, company.Teams.Where(team => _teamService.GetExtraMaturity(team) == 7).ToList().Count);

        }

        private static Team CreateTeamWithDiferentsEmployeesMaturity()
        {
            var employeesTeam3_level1 = Builder<Employee>.CreateListOfSize(2).All()
                     .With(employee => employee.ProgressionLevel = 1)
                     .Build();
            var employeesTeam3_level2 = Builder<Employee>.CreateListOfSize(2).All()
                  .With(employee => employee.ProgressionLevel = 2)
                  .Build();
            var employeesTeam3_level4 = Builder<Employee>.CreateNew()
                 .With(employee => employee.ProgressionLevel = 4)
                 .Build();
            var employeesTeam3_level5 = Builder<Employee>.CreateNew()
                .With(employee => employee.ProgressionLevel = 5)
                .Build();

            var employeesTeam3 = new List<Employee>();
            employeesTeam3.AddRange(employeesTeam3_level1);
            employeesTeam3.AddRange(employeesTeam3_level2);
            employeesTeam3.Add(employeesTeam3_level4);
            employeesTeam3.Add(employeesTeam3_level5);

            var team3 = Builder<Team>.CreateNew()
              .With(newTeam => newTeam.MinimunMaturity = 5)
              .With(newTeam => newTeam.Name = CompanyFaker.Name())
              .With(newTeam => newTeam.Employees = employeesTeam3)
              .Build();
            return team3;
        }

        private static Team CreateTeamFourExtraMaturity()
        {
            var employeesTeam1 = Builder<Employee>.CreateListOfSize(9).All()
                     .With(employee => employee.ProgressionLevel = 1)
                     .With(employee => employee.Name = NameFaker.Name())
                     .With(employee => employee.BirthYear = 1994)
                     .With(employee => employee.AdmissionYear = 2018)
                     .With(employee => employee.LastProgressionYear = 2018)
                     .Build();

            var team1 = Builder<Team>.CreateNew()
                .With(newTeam => newTeam.MinimunMaturity = 5)
                .With(newTeam => newTeam.Name = CompanyFaker.Name())
                .With(newTeam => newTeam.Employees = employeesTeam1)
                .Build();
            return team1;
        }
    }
}
