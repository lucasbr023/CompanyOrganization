using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class TeamService 
    {
        public List<Team> CreateTeams(IList<string> teamsLines)
        {
            var teams = new List<Team>();
            foreach (var line in teamsLines)
            {
                var splitLine = line.Split(Constants.SEMICOLON).ToList();
                ValidateTeamLine(splitLine);

                teams.Add(new Team()
                {
                    Name = splitLine[Constants.INDEX_NAME_TEAM],
                    MinimunMaturity = Util.ConvertStringToInt(splitLine[Constants.INDEX_MINIMUM_MATURITY_TEAM]),
                });
            }
            return teams;
        }

        public int GetCurrentMaturity(Team team)
        {
            return team.Employees.Sum(employee => employee.ProgressionLevel);
        }

        public int GetExtraMaturity(Team team)
        {
            return GetCurrentMaturity(team) - team.MinimunMaturity;
        }

        private void ValidateTeamLine(IList<string> splitLine)
        {
            if (splitLine.ElementAtOrDefault(Constants.INDEX_NAME_TEAM) == null
                || splitLine.ElementAtOrDefault(Constants.INDEX_MINIMUM_MATURITY_TEAM) == null)
            {
                throw new Exception(string.Format(Messages.FileWithInvalidValues, "Team"));
            }
        }
    }
}
