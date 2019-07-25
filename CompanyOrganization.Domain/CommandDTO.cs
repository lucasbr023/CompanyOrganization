using System.Collections.Generic;
using CompanyEmployeesBalancing.Domain.Enumeration;

namespace CompanyEmployeesBalancing.Domain
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
