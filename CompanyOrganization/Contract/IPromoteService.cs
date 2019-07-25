using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Contract
{
    public interface IPromoteService
    {
        
        /// <summary>
        /// Promote employees
        /// </summary>
        /// <param name="numberEmployeesToPromote">number of employees to promote</param>
        void Promote(int numberEmployeesToPromote);

        /// <summary>
        /// Get employees to promote
        /// </summary>
        /// <param name="numberEmployeesToPromote">number employees to promote</param>
        /// <returns></returns>
        IList<Employee> GetEmployeesToPromote(int numberEmployeesToPromote);

        /// <summary>
        /// string containing information of promotion
        /// </summary>
        /// <param name="employees">list of employees</param>
        /// <returns></returns>
        string PromoteToString(IList<Employee> employees);
    }
}
