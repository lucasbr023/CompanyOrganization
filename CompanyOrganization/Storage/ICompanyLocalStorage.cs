using CompanyOrganization.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyOrganization.Storage
{
    public interface ICompanyLocalStorage
    {
        IList<Team> GetTeams();
        void UpdateTeams(IList<Team> teamsToAdd);
        IList<Employee> GetEmployees();
        void UpdateEmployees(IList<Employee> employeesToAdd);
        Company CreateCompany();
        void UpdateCompany(Company companyToUpdate);
    }
}
