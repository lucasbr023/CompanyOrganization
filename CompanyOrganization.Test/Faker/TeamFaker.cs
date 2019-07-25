using System.Collections.Generic;
using CompanyEmployeesBalancing.Domain.BusinessObjects;
using Faker;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Faker
{
    [TestClass]
    public static class TeamFaker
    {

        public static IList<Team> GetTeamsWithoutEmployee(int numberOfTeams)
        {
            return Builder<Team>.CreateListOfSize(numberOfTeams)
                .All()
                .With(team => team.MinimunMaturity = NumberFaker.Number(1, 5))
                .With(team => team.Name = CompanyFaker.Name())
                .Build();
        }

        public static IList<Team> GetCompleteTeams()
        {
            return Builder<Team>.CreateListOfSize(5)
                .All()
                .With(team => team.MinimunMaturity = NumberFaker.Number(1, 5))
                .With(team => team.Name = CompanyFaker.Name())
                .With(team => team.Employees = Builder<Employee>.CreateListOfSize(5).All()
                    .With(employee => employee.ProgressionLevel = NumberFaker.Number(1, 5))
                    .With(employee => employee.Name = NameFaker.Name())
                    .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
                    .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
                    .Build())
                .Build();
        }

        public static Team GetIncompleteTeam()
        {
            return Builder<Team>.CreateNew()
               .With(team => team.MinimunMaturity = 10)
               .With(team => team.Name = CompanyFaker.Name())
               .With(team => team.Employees = Builder<Employee>.CreateListOfSize(3).All()
                   .With(employee => employee.ProgressionLevel = NumberFaker.Number(1, 3))
                   .With(employee => employee.Name = NameFaker.Name())
                   .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
                   .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
                   .Build())
               .Build();
        }
    }
}
