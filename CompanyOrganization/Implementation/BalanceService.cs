using CompanyEmployeesBalancing.Domain.BusinessObjects;
using CompanyEmployeesBalancing.Resources;
using CompanyEmployeesBalancing.Services.Contract;
using CompanyEmployeesBalancing.Services.Storage;
using System;
using System.Linq;

namespace CompanyEmployeesBalancing.Services.Implementation
{
    public class BalanceService : IBalanceService
    {
        private readonly ITeamService _teamService;
        
        public BalanceService(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public void Balance()
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
        }

        private void ValidateTeamsWithLessThanMinimumMaturity(Company company)
        {
            if (company.Teams.Any(team => _teamService.GetExtraMaturity(team) < 0))
            {
                throw new Exception(Messages.TeamsHaventEnoughMaturity);
            }
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
            return _teamService.GetExtraMaturity(teamMaximumMaturity) - _teamService.GetExtraMaturity(teamMinimumMaturity);
        }

        private Team GetTeamMinimumMaturity(Company company)
        {
            return company.Teams.OrderBy(team => _teamService.GetExtraMaturity(team)).FirstOrDefault();
        }

        private Team GetTeamMaximumMaturity(Company company)
        {
            var maxMaturity = company.Teams.Max(team => _teamService.GetExtraMaturity(team));

            var teams = company.Teams.Where(team => _teamService.GetExtraMaturity(team) == maxMaturity).ToList();

            if (teams.Count >= 2)
            {
                teams = teams.OrderBy(team => team.Employees.Min(x => x.ProgressionLevel)).ToList();
            }

            return teams.ToList().FirstOrDefault();
        }

        public string BalanceToString(Company company)
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
                    {
                        toString += $"{employee.Name} " +
                                    $"- {employee.ProgressionLevel} \n";
                    }
                    toString += "\n";
                }
            }
            return toString;
        }
    }
}