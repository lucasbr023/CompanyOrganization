using CompanyOrganization.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyOrganization.Contract
{
    public interface IEmployeeService
    {

        /// <summary>
        /// Create list of employees
        /// </summary>
        /// <param name="employeesLines">lines from csv containig employee</param>
        /// <returns>List of employee</returns>
        List<Employee> CreateEmployees(IList<string> employeesLines);
    }
}
