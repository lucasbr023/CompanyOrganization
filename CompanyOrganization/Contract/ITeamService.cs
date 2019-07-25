using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Contract
{
    public interface ITeamService
    {
        /// <summary>
        /// Create list of teams
        /// </summary>
        /// <param name="teamsLines">lines from csv containing teams</param>
        /// <returns>List of teams</returns>
        List<Team> CreateTeams(IList<string> teamsLines);

        /// <summary>
        /// get value of extra maturity
        /// </summary>
        /// <param name="team">team</param>
        /// <returns>value of extra maturity</returns>
        int GetExtraMaturity(Team team);
    }
}
