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
    //[TestClass]
    //public class AllocateServiceTest
    //{
    //    private readonly Allocate _allocateService;
    //    private readonly CompanyOrganization.Implementation.Team _teamService;
        
    //    public AllocateServiceTest()
    //    {
    //        _allocateService = DependencyResolver.Get<Allocate>();
    //        _teamService = DependencyResolver.Get<CompanyOrganization.Implementation.Team>();
    //    }

    //    [TestMethod]
    //    public void TestAllocateInsufficientEmployeesToFillTeams()
    //    {
    //        var teams = Builder<Team>.CreateListOfSize(5)
    //            .All()
    //            .With(team => team.MinimunMaturity = NumberFaker.Number(1, 5))
    //            .With(team => team.Name = CompanyFaker.Name())
    //            .Build();

    //        var employees = Builder<Employee>.CreateListOfSize(5).All()
    //                .With(employee => employee.ProgressionLevel = 1)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
    //                .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
    //                .Build();

    //        var exception = Assert.ThrowsException<Exception>(() => _allocateService.Allocate
    //            (teams, employees));
    //        Assert.AreEqual(Messages.TeamsHaventEnoughMaturity, exception.Message);
    //    }

    //    [TestMethod]
    //    public void TestAllocateWithExactlyEmployeesToFillTeams()
    //    {
    //        var teams = Builder<Team>.CreateListOfSize(3)
    //            .All()
    //            .With(team => team.MinimunMaturity = 4)
    //            .With(team => team.Name = CompanyFaker.Name())
    //            .Build();

    //        var employees = Builder<Employee>.CreateListOfSize(12).All()
    //                .With(employee => employee.ProgressionLevel = 1)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
    //                .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
    //                .Build();

    //        _allocateService.Allocate(teams, employees);

    //        var company = CompanyLocalStorage.GetInstance.GetCompany();
    //        Assert.AreEqual(false, company.Teams.Any(team => _teamService.GetExtraMaturity(team) > 0));
    //    }

    //    [TestMethod]
    //    public void TestAllocateWithOneEmployeesToFillTeamExtraMaturityTeam()
    //    {
    //        var teams = Builder<Team>.CreateListOfSize(3)
    //            .All()
    //            .With(team => team.MinimunMaturity = 4)
    //            .With(team => team.Name = CompanyFaker.Name())
    //            .Build();

    //        var employees = Builder<Employee>.CreateListOfSize(13).All()
    //                .With(employee => employee.ProgressionLevel = 1)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
    //                .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
    //                .Build();

    //        _allocateService.Allocate(teams, employees);

    //        var company = CompanyLocalStorage.GetInstance.GetCompany();
    //        Assert.AreEqual(1, company.Teams.Count(team => _teamService.GetExtraMaturity(team) == 1));
    //    }
        
    //    [TestMethod]
    //    public void TestAllocateEmployeesWithoutTeam()
    //    {
    //        var teams = new List<CompanyOrganization.Domain.BusinessObjects.Team>();

    //        var employees = Builder<Employee>.CreateListOfSize(13).All()
    //                .With(employee => employee.ProgressionLevel = 1)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
    //                .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
    //                .Build();

    //        var exception = Assert.ThrowsException<Exception>(() => _allocateService.Allocate(teams, employees));
    //        Assert.AreEqual(Messages.CompanyWithoutTeams, exception.Message);
    //    }

    //    [TestMethod]
    //    public void TestAllocateTeamWithoutEmployees()
    //    {
    //        var teams = Builder<Team>.CreateListOfSize(3)
    //            .All()
    //            .With(team => team.MinimunMaturity = 4)
    //            .With(team => team.Name = CompanyFaker.Name())
    //            .Build();

    //        var employees = new List<CompanyOrganization.Domain.BusinessObjects.Employee>();

    //        var exception = Assert.ThrowsException<Exception>(() => _allocateService.Allocate(teams, employees));
    //        Assert.AreEqual(Messages.TeamsHaventEnoughMaturity, exception.Message);
    //    }

    //    [TestMethod]
    //    public void TestAllocateWithoutTeamEmployees()
    //    {
    //        var exception = Assert.ThrowsException<Exception>(() => _allocateService.Allocate(new List<CompanyOrganization.Domain.BusinessObjects.Team>(), new List<CompanyOrganization.Domain.BusinessObjects.Employee>()));
    //        Assert.AreEqual(Messages.CompanyWithoutTeams, exception.Message);
    //    }

    //    [TestMethod]
    //    public void TestAllocateWithTwoEmployeesToFillTeamExtraTeam()
    //    {
    //        var teams = Builder<Team>.CreateListOfSize(3)
    //            .All()
    //            .With(team => team.MinimunMaturity = 4)
    //            .With(team => team.Name = CompanyFaker.Name())
    //            .Build();

    //        var employees = Builder<Employee>.CreateListOfSize(14).All()
    //                .With(employee => employee.ProgressionLevel = 1)
    //                .With(employee => employee.Name = NameFaker.Name())
    //                .With(employee => employee.BirthYear = NumberFaker.Number(1980, 2000))
    //                .With(employee => employee.AdmissionYear = NumberFaker.Number(2010, 2018))
    //                .Build();

    //        _allocateService.Allocate(teams, employees);

    //        var company = CompanyLocalStorage.GetInstance.GetCompany();
    //        Assert.AreEqual(2, company.Teams.Where(team => _teamService.GetExtraMaturity(team) == 1).Count());
    //    }


    //}
}
