using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Storage
{
    public interface ICompanyLocalStorage
    {
        /// <summary>
        /// Get teams
        /// </summary>
        /// <returns>list with teams</returns>
        IList<Team> GetTeams();

        /// <summary>
        /// update teams
        /// </summary>
        /// <param name="teamsToAdd">teams to add</param>
        void UpdateTeams(IList<Team> teamsToAdd);

        /// <summary>
        /// Get employees
        /// </summary>
        /// <returns>list of employees</returns>
        IList<Employee> GetEmployees();

        /// <summary>
        /// Update employees
        /// </summary>
        /// <param name="employeesToAdd">employees to add</param>
        void UpdateEmployees(IList<Employee> employeesToAdd);

        /// <summary>
        ///Get company
        /// </summary>
        /// <returns>company</returns>
        Company GetCompany();

        /// <summary>
        /// Create company
        /// </summary>
        /// <returns>company</returns>
        Company CreateCompany();

        /// <summary>
        /// Update company
        /// </summary>
        /// <param name="companyToUpdate">company</param>
        void UpdateCompany(Company companyToUpdate);
    }
}
