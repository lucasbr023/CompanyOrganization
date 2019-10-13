using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Domain.BusinessObjects
{
    public class Team
    {
        public string Name { get; set; }

        public IList<Employee> Employees { get; set; }

        public int MinimunMaturity { get; set; }
        
        public Team(string line)
        {
            Employees = new List<Employee>();
            try
            {
                var splitLine = line.Split(Constants.SEMICOLON).ToList();
                Name = splitLine[Constants.INDEX_NAME_TEAM];
                MinimunMaturity = Util.ConvertStringToInt(splitLine[Constants.INDEX_MINIMUM_MATURITY_TEAM]);
            }
            catch (Exception)
            {
                throw new Exception(string.Format(Messages.FileWithInvalidValues, "Team"));
            }
        }

        public int GetCurrentMaturity()
        {
            return Employees.Sum(employee => employee.ProgressionLevel);
        }

        public int GetExtraMaturity()
        {
            return GetCurrentMaturity() - MinimunMaturity;
        }
    }
}
