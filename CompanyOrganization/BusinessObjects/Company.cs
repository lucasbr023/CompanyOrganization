using System.Collections.Generic;

namespace CompanyOrganization.Domain.BusinessObjects
{
    public class Company
    {
        public IList<Team> Teams { get; set; }

        public Company()
        {
            Teams = new List<Team>();
        }
    }
}
