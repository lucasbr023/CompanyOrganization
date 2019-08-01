﻿using CompanyEmployeesBalancing.Test.Faker;
using CompanyOrganization;
using CompanyOrganization.Implementation;
using CompanyOrganization.Test;
using Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployeesBalancing.Test.Core
{
    //[TestClass]
    //public class EmployeeServiceTest
    //{
    //    private readonly Employee _employeeService;
    //    public EmployeeServiceTest()
    //    {
    //        _employeeService = DependencyResolver.Get<Employee>();
    //    }

    //    [TestMethod]
    //    public void TestCreateEmployee()
    //    {
    //        var teams = _employeeService.CreateEmployees(FileFaker.GetEmployeeLines());
    //        Assert.AreEqual(100, teams.Count);
    //    }

    //    [TestMethod]
    //    public void TestCreateEmployeesWithInvalidValues()
    //    {
    //        Exception ex = Assert.ThrowsException<Exception>(() => _employeeService.CreateEmployees(FileFaker.GetLinesWithInvalidValueEmployee()));
    //        Assert.AreEqual(Messages.InvalidValue, ex.Message);
    //    }

    //    [TestMethod]
    //    public void TestCreateEmployeesWithOnlyName()
    //    {
    //        Exception ex = Assert.ThrowsException<Exception>(() => _employeeService.CreateEmployees(FileFaker.GetLinesWithOnlyName()));
    //        Assert.AreEqual(string.Format(Messages.FileWithInvalidValues, "Employee"), ex.Message);
    //    }

    //    [TestMethod]
    //    public void TestCreateEmployeesWithNoLines()
    //    {
    //        var employees = _employeeService.CreateEmployees(new List<string>());
    //        Assert.AreEqual(0, employees.Count);
    //    }

    //    [TestMethod]
    //    public void TestCreateEmployeesWithEmptyLineInTheMiddle()
    //    {
    //        Exception ex = Assert.ThrowsException<Exception>(() => _employeeService.CreateEmployees(FileFaker.GetEmployeeLinesWithEmptyLine()));
    //        Assert.AreEqual(string.Format(Messages.FileWithInvalidValues, "Employee"), ex.Message);
    //    }

    //    [TestMethod]
    //    public void CreateEmployeeWithEmployeeWithoutLastProgressionYear()
    //    {
    //        var employeesLines = new List<string>();
    //        employeesLines.Add($"{NameFaker.Name()};" +
    //                           $"{NumberFaker.Number(1, 5)};" +
    //                           $"{NumberFaker.Number(1970, 2000)};" +
    //                           $"{2011};");

    //        var employees = _employeeService.CreateEmployees(employeesLines);
    //        Assert.AreEqual(2011, employees.FirstOrDefault().LastProgressionYear);

    //    }
    //}
}