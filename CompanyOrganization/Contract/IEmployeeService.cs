using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Contract
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
