using CompanyOrganization.Contract;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Storage;
using System;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class Balance : ICommand
    {
        public string Execute(string parameters = null)
        {
            var company = CompanyLocalStorage.GetInstance.GetCompany();
            if (company.Teams.Any())
            {
                ValidateTeamsWithLessThanMinimumMaturity(company);

                var teamMaximumMaturity = GetTeamMaximumMaturity(company);
                var teamMinimumMaturity = GetTeamMinimumMaturity(company);

                var difference = GetMaturityDifferenceBetweenTeams(teamMaximumMaturity, teamMinimumMaturity);
                var employeeToTransfer = GetEmployeeToTransferToAnotherTeam(teamMaximumMaturity, difference);

                while (difference > 1 && employeeToTransfer != null)
                {
                    MoveEmployeeBetweenTeams(teamMaximumMaturity, teamMinimumMaturity, employeeToTransfer);

                    teamMaximumMaturity = GetTeamMaximumMaturity(company);
                    teamMinimumMaturity = GetTeamMinimumMaturity(company);

                    difference = GetMaturityDifferenceBetweenTeams(teamMaximumMaturity, teamMinimumMaturity);
                    employeeToTransfer = GetEmployeeToTransferToAnotherTeam(teamMaximumMaturity, difference);
                }
            }
            CompanyLocalStorage.GetInstance.UpdateCompany(company);
            return ToString(company);
        }

        private void ValidateTeamsWithLessThanMinimumMaturity(Company company)
        {
            if (company.Teams.Any(team => team.GetExtraMaturity() < 0))
                throw new Exception(Messages.TeamsHaventEnoughMaturity);
        }

        private void MoveEmployeeBetweenTeams(Team teamFrom, Team teamTo, Employee employee)
        {
            teamTo.Employees.Add(employee);
            teamFrom.Employees.Remove(employee);
        }

        private Employee GetEmployeeToTransferToAnotherTeam(Team teamMaximumMaturity, int difference)
        {
            return teamMaximumMaturity
                   .Employees
                   .Where(employee => employee.ProgressionLevel < difference)
                   .OrderBy(employee => employee.ProgressionLevel)
                   .FirstOrDefault();
        }

        private int GetMaturityDifferenceBetweenTeams(Team teamMaximumMaturity, Team teamMinimumMaturity)
        {
            return teamMaximumMaturity.GetExtraMaturity() - teamMinimumMaturity.GetExtraMaturity();
        }

        private Team GetTeamMinimumMaturity(Company company)
        {
            return company.Teams.OrderBy(team => team.GetExtraMaturity()).FirstOrDefault();
        }

        private Team GetTeamMaximumMaturity(Company company)
        {
            var maxMaturity = company.Teams.Max(team => team.GetExtraMaturity());

            var teams = company.Teams.Where(team => team.GetExtraMaturity() == maxMaturity).ToList();

            if (teams.Count >= 2)
                teams = teams.OrderBy(team => team.Employees.Min(x => x.ProgressionLevel)).ToList();

            return teams.ToList().FirstOrDefault();
        }

        private string ToString(Company company)
        {
            var toString = "===============BALANCE=============== \n";
            if (company.Teams.Any())
            {
                foreach (var team in company.Teams)
                {
                    toString += $"{team.Name}   " +
                                $"- Min. Maturity {team.MinimunMaturity} " +
                                $"- Current Maturity {team.Employees.Sum(e => e.ProgressionLevel)} \n";

                    foreach (var employee in team.Employees)
                        toString += $"{employee.Name} " +
                                    $"- {employee.ProgressionLevel} \n";
                    toString += "\n";
                }
            }
            return toString;
        }
    }
}