using CompanyEmployeesBalancing.Domain.BusinessObjects;
using System.Collections.Generic;

namespace CompanyEmployeesBalancing.Services.Storage
{
    public class CompanyLocalStorage : ICompanyLocalStorage
    {
        private Company company;
        private IList<Team> teams;
        private IList<Employee> employees;


        public CompanyLocalStorage()
        {
            teams = new List<Team>();
            employees = new List<Employee>();
        }

        public static CompanyLocalStorage _instance;

        private static readonly object _lock = new object();

        public static CompanyLocalStorage GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CompanyLocalStorage();
                        }
                    }
                }
                return _instance;
            }
        }

        public IList<Team> GetTeams()
        {
            return teams;
        }

        public void UpdateTeams(IList<Team> teamsToAdd)
        {
            teams = teamsToAdd;
        }
        
        public IList<Employee> GetEmployees()
        {
            return employees;
        }

        public void UpdateEmployees(IList<Employee> employeesToAdd)
        {
            employees = employeesToAdd;
        }

        public Company GetCompany()
        {
            return company;
        }

        public Company CreateCompany()
        {
            company = new Company();
            foreach (var team in teams)
            {
                team.Employees = new List<Employee>();
            }
            return company;
        }

        public void UpdateCompany(Company companyToUpdate)
        {
            company = companyToUpdate;
        }
    }
}
