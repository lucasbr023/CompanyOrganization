using System.Collections.Generic;
using CompanyEmployeesBalancing.Domain.BusinessObjects;
using Faker;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Faker
{
    [TestClass]
    public static class EmployeeFaker
    {
        public static IList<Employee> GetEmployees()
        {
            return Builder<Employee>.CreateListOfSize(20).All()
                    .With(employee => employee.ProgressionLevel = NumberFaker.Number(1, 5))
                    .With(employee => employee.Name = NameFaker.Name())
                    .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
                    .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
                    .Build();
        }

        public static IList<Employee> GetInsufficientEmployeesWithToFillTeams()
        {
            return Builder<Employee>.CreateListOfSize(5).All()
                    .With(employee => employee.ProgressionLevel = 1)
                    .With(employee => employee.Name = NameFaker.Name())
                    .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
                    .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
                    .Build();
        }
    }
}
