using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Contract;
using CompanyOrganization.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class Allocate : ICommand
    {
        public string Execute(string parameters = null)
        {
            var companyLocalStorage = CompanyLocalStorage.GetInstance;
            var company = companyLocalStorage.GetCompany();
            company.Teams = companyLocalStorage.GetTeams();
            ValidateCompany(company);

            AddEmployeesToTeams(companyLocalStorage.GetEmployees(), company);

            ValidateTeamsMinimumMaturity(company);
            CompanyLocalStorage.GetInstance.UpdateCompany(company);

            return ToString(company);
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
            if (company.Teams.Any(team => team.GetExtraMaturity() < 0))
                throw new Exception(Messages.TeamsHaventEnoughMaturity);
        }

        private void ValidateCompany(Company company)
        {
            if (!company.Teams.Any())
                throw new Exception(Messages.CompanyWithoutTeams);
        }

        private Team GetBestTeamToAllocate(Company company, Employee employee)
        {
            var allocateTeam = GetTeamWithLessThanMinimumMaturity(company, employee);

            if (allocateTeam == null)
                allocateTeam = GetTeamWithLessRequiredMinimumMaturity(company);
            return allocateTeam;
        }

        private Team GetTeamWithLessThanMinimumMaturity(Company company, Employee employee)
        {
            return company.Teams.FirstOrDefault(team =>
                                                Math.Abs(team.GetExtraMaturity()) >= employee.ProgressionLevel
                                                && team.GetExtraMaturity() < 0);
        }

        private Team GetTeamWithLessRequiredMinimumMaturity(Company company)
        {
            return company.Teams.OrderBy(team => team.GetExtraMaturity()).FirstOrDefault();
        }

        public string ToString(Company company)
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
                        toString += $"{employee.Name} - {employee.ProgressionLevel} \n";

                    toString += "\n";
                }
            }
            return toString;
        }
    }
}