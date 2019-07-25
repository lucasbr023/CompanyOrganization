using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Contract
{
    public interface IAllocateService
    {
        /// <summary>
        /// Allocate teams and employees inside company.
        /// </summary>
        /// <param name="teams">teams</param>
        /// <param name="employees">employees</param>
        /// <returns>Company</returns>
        void Allocate(IList<Team> teams, IList<Employee> employees);

        /// <summary>
        /// string containing information of allocation of team and employees inside company
        /// </summary>
        /// <param name="company">company</param>
        /// <returns></returns>
        string AllocateToString(Company company);
    }
}
