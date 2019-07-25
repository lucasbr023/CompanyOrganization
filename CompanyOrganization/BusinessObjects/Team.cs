using System.Collections.Generic;

namespace CompanyOrganization.Domain.BusinessObjects
{
    public class Team
    {
        public string Name { get; set; }

        public IList<Employee> Employees { get; set; }

        public int MinimunMaturity { get; set; }
        
        public Team()
        {
            Employees = new List<Employee>();
        }
    }
}
