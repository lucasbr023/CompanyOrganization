using CompanyOrganization;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Implementation;
using CompanyOrganization.Storage;
using CompanyOrganization.Test;
using CompanyOrganization.Utils;
using Faker;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CompanyEmployeesBalancing.Test.Core
{
    //[TestClass]
    //public class PromoteServiceTest
    //{
    //    private readonly Promote _promoteService;
    //    public PromoteServiceTest()
    //    {
    //        _promoteService = DependencyResolver.Get<Promote>();
    //    }
    //    [TestMethod]
    //    public void PromoteOneEmployee()
    //    {
    //        CurrentYear.GetInstance.UpdateYear(DateTime.Now.Year);
    //        var nameEmployeePromoted = "Employee promoted";
    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //                .With(employee => employee.ProgressionLevel = 3)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = 1994)
    //                .With(employee => employee.AdmissionYear = 2018)
    //                .With(employee => employee.LastProgressionYear = 2018)
    //                .Build(); 

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 2)
    //             .With(employee => employee.Name = nameEmployeePromoted)
    //             .With(employee => employee.BirthYear = 1970)
    //             .With(employee => employee.AdmissionYear = 2010)
    //             .With(employee => employee.LastProgressionYear = 2012).Build());
            
    //        CompanyLocalStorage.GetInstance.UpdateEmployees(employees);

    //        _promoteService.Promote(1);
    //        var employeePromoted = employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted));
    //        Assert.AreEqual(3, employeePromoted.ProgressionLevel);
    //        Assert.AreEqual(DateTime.Now.Year, employeePromoted.LastProgressionYear);
    //        Assert.AreEqual(DateTime.Now.AddYears(1).Year, CurrentYear.GetInstance.Year);
    //    }

    //    [TestMethod]
    //    public void PromoteTwoEmployees()
    //    {
    //        CurrentYear.GetInstance.UpdateYear(DateTime.Now.Year);
    //        var nameEmployeePromoted1 = "Employee promoted 1";
    //        var nameEmployeePromoted2 = "Employee promoted 2";
    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //                .With(employee => employee.ProgressionLevel = 3)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = 1994)
    //                .With(employee => employee.AdmissionYear = 2018)
    //                .With(employee => employee.LastProgressionYear = 2018)
    //                .Build(); 

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 2)
    //             .With(employee => employee.Name = nameEmployeePromoted1)
    //             .With(employee => employee.BirthYear = 1970)
    //             .With(employee => employee.AdmissionYear = 2010)
    //             .With(employee => employee.LastProgressionYear = 2012).Build());

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 3)
    //             .With(employee => employee.Name = nameEmployeePromoted2)
    //             .With(employee => employee.BirthYear = 1970)
    //             .With(employee => employee.AdmissionYear = 2010)
    //             .With(employee => employee.LastProgressionYear = 2012).Build());
            
    //        CompanyLocalStorage.GetInstance.UpdateEmployees(employees);

    //        _promoteService.Promote(2);
    //        Assert.AreEqual(3, employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted1)).ProgressionLevel);
    //        Assert.AreEqual(4, employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted2)).ProgressionLevel);
    //        Assert.AreEqual(2020, CurrentYear.GetInstance.Year);
    //    }

    //    [TestMethod]
    //    public void TwoPromoteTwoEmployees_AddTwoYears()
    //    {
    //        CurrentYear.GetInstance.UpdateYear(DateTime.Now.Year);
    //        var nameEmployeePromoted1 = "Employee promoted 1";
    //        var nameEmployeePromoted2 = "Employee promoted 2";
    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //                .With(employee => employee.ProgressionLevel = 3)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = 1994)
    //                .With(employee => employee.AdmissionYear = 2018)
    //                .With(employee => employee.LastProgressionYear = 2018)
    //                .Build();

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 2)
    //             .With(employee => employee.Name = nameEmployeePromoted1)
    //             .With(employee => employee.BirthYear = 1970)
    //             .With(employee => employee.AdmissionYear = 2010)
    //             .With(employee => employee.LastProgressionYear = 2012).Build());

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 3)
    //             .With(employee => employee.Name = nameEmployeePromoted2)
    //             .With(employee => employee.BirthYear = 1971)
    //             .With(employee => employee.AdmissionYear = 2011)
    //             .With(employee => employee.LastProgressionYear = 2013).Build());

    //        CompanyLocalStorage.GetInstance.UpdateEmployees(employees);

    //        _promoteService.Promote(1);
    //        Assert.AreEqual(3, employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted1)).ProgressionLevel);
    //        Assert.AreEqual(DateTime.Now.AddYears(1).Year, CurrentYear.GetInstance.Year);
    //        _promoteService.Promote(1);
    //        Assert.AreEqual(4, employees.FirstOrDefault(employee => employee.Name.Equals(nameEmployeePromoted2)).ProgressionLevel);
    //        Assert.AreEqual(DateTime.Now.AddYears(2).Year, CurrentYear.GetInstance.Year);
    //    }

    //    [TestMethod]
    //    public void PromoteAddYear()
    //    {
    //        CurrentYear.GetInstance.UpdateYear(DateTime.Now.Year);
    //        var nameEmployeePromoted = "Employee promoted";
    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //                .With(employee => employee.ProgressionLevel = 3)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = 1994)
    //                .With(employee => employee.AdmissionYear = 2018)
    //                .With(employee => employee.LastProgressionYear = 2018)
    //                .Build(); 

    //        employees.Add(Builder<Employee>.CreateNew()
    //             .With(employee => employee.ProgressionLevel = 2)
    //             .With(employee => employee.Name = nameEmployeePromoted)
    //             .With(employee => employee.BirthYear = 1970)
    //             .With(employee => employee.AdmissionYear = 2010)
    //             .With(employee => employee.LastProgressionYear = 2012).Build());
            
    //        CompanyLocalStorage.GetInstance.UpdateEmployees(employees);

    //        _promoteService.Promote(1);
    //        Assert.AreEqual(2020, CurrentYear.GetInstance.Year);
    //    }

    //    [TestMethod]
    //    public void PromoteNoneEmployee()
    //    {
    //        var exception = Assert.ThrowsException<Exception>(() => _promoteService.Promote(0));
    //        Assert.AreEqual(Messages.NoEmployeeToPromote, exception.Message);
    //    }

    //    [TestMethod]
    //    public void PromoteAllEmployeeLevelFive()
    //    {
    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //            .With(employee => employee.ProgressionLevel = 5)
    //            .With(employee => employee.Name = NameFaker.Name())
    //            .With(employee => employee.BirthYear = 1994)
    //            .With(employee => employee.AdmissionYear = 2018)
    //            .With(employee => employee.LastProgressionYear = 2018)
    //            .Build();
    //        CompanyLocalStorage.GetInstance.UpdateEmployees(employees);

    //        var exception = Assert.ThrowsException<Exception>(() => _promoteService.Promote(3));
    //        Assert.AreEqual(Messages.NoEmployeeToPromote, exception.Message);
    //    }
    //}
}
