using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Contract;
using CompanyOrganization.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class AllocateService : IAllocateService
    {
        private readonly ITeamService _teamService;

        public AllocateService(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public void Allocate(IList<Team> teams, IList<Employee> employees)
        {
            var company = CompanyLocalStorage.GetInstance.CreateCompany();
            company.Teams = teams;
            ValidateCompany(company);

            AddEmployeesToTeams(employees, company);

            ValidateTeamsMinimumMaturity(company);
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
        }

        private void AddEmployeesToTeams(IList<Employee> employees, Company company)
        {
            foreach (var employee in employees.OrderByDescending(e => e.ProgressionLevel))
            {
                var team = GetBestTeamToAllocate(company, employee);
                team.Employees.Add(employee);
            }
        }

        private void ValidateTeamsMinimumMaturity(Company company)
        {
            if (company.Teams.Any(team => _teamService.GetExtraMaturity(team) < 0))
            {
                throw new Exception(Messages.TeamsHaventEnoughMaturity);
            }
        }

        private void ValidateCompany(Company company)
        {
            if (!company.Teams.Any())
            {
                throw new Exception(Messages.CompanyWithoutTeams);
            }
        }

        private Team GetBestTeamToAllocate(Company company, Employee employee)
        {
            var allocateTeam = GetTeamWithLessThanMinimumMaturity(company, employee);

            if (allocateTeam == null)
            {
                allocateTeam = GetTeamWithLessRequiredMinimumMaturity(company);
            }
            return allocateTeam;
        }

        private Team GetTeamWithLessThanMinimumMaturity(Company company, Employee employee)
        {
            return company.Teams.FirstOrDefault(team =>
            Math.Abs(_teamService.GetExtraMaturity(team)) >= employee.ProgressionLevel
            && _teamService.GetExtraMaturity(team) < 0);
        }

        private Team GetTeamWithLessRequiredMinimumMaturity(Company company)
        {
            return company.Teams.OrderBy(team => _teamService.GetExtraMaturity(team)).FirstOrDefault();
        }

        public string AllocateToString(Company company)
        {
            var toString = "===============ALLOCATE=============== \n";
            if (company.Teams.Any())
            {
                foreach (var team in company.Teams)
                {
                    toString +=
                        $"{team.Name} " +
                        $"- Min. Maturity {team.MinimunMaturity}" +
                        $" - Current Maturity {team.Employees.Sum(e => e.ProgressionLevel)} \n ";

                    foreach (var employee in team.Employees)
                    {
                        toString += $"{employee.Name} - {employee.ProgressionLevel} \n";
                    }
                    toString += "\n";
                }
            }
            return toString;
        }
    }
}
