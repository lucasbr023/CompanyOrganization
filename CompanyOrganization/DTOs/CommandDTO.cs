using CompanyOrganization.Enumeration;
using System.Collections.Generic;

namespace CompanyOrganization.DTOs
{
    public class CommandDto
    {
        public CommandEnum Command { get; set; }
        public List<string> Arguments { get; set; }

        public CommandDto()
        {
            Arguments = new List<string>();
        }
    }
}
