using System;
using System.Collections.Generic;
using CompanyEmployeesBalancing.Resources;
using CompanyEmployeesBalancing.Services.Implementation;
using CompanyEmployeesBalancing.Test.Configuration;
using CompanyEmployeesBalancing.Test.Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Core
{
    [TestClass]
    public class TeamServiceTest
    {
        private readonly TeamService _teamService;
        public TeamServiceTest()
        {
            _teamService = DependencyResolver.Get<TeamService>();
        }

        [TestMethod]
        public void TestCreateTeams()
        {
            var teams = _teamService.CreateTeams(FileFaker.GetTeamsLines());
            Assert.AreEqual(10, teams.Count);
        }

        [TestMethod]
        public void TestCreateTeamsWithInvalidValues()
        {
            Exception ex = Assert.ThrowsException<Exception>(() => _teamService.CreateTeams(FileFaker.GetLinesWithInvalidValueTeams()));
            Assert.AreEqual(Messages.InvalidValue, ex.Message);
        }

        [TestMethod]
        public void TestCreateTeamsWithOnlyName()
        {
            Exception ex = Assert.ThrowsException<Exception>(() => _teamService.CreateTeams(FileFaker.GetLinesWithOnlyName()));
            Assert.AreEqual(string.Format(Messages.FileWithInvalidValues, "Team"), ex.Message);
        }


        [TestMethod]
        public void TestCreateTeamWithNoLines()
        {
            var employees = _teamService.CreateTeams(new List<string>());
            Assert.AreEqual(0, employees.Count);
        }

        [TestMethod]
        public void TestCreateEmployeesWithEmptyLineInTheMiddle()
        {
            Exception ex = Assert.ThrowsException<Exception>(() => _teamService.CreateTeams(FileFaker.GetTeamsLinesWithEmptyLine()));
            Assert.AreEqual(string.Format(Messages.FileWithInvalidValues, "Team"), ex.Message);
        }
    }
}
