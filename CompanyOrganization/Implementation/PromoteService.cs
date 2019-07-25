using CompanyOrganization.Contract;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Storage;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class PromoteService : IPromoteService
    {
        public void Promote(int numberEmployeesToPromote)
        {
            var employeesToPromote = GetEmployeesToPromote(numberEmployeesToPromote);

            ValidateEmployeesToPromote(employeesToPromote);

            foreach (var employeeToPromote in employeesToPromote)
            {
                employeeToPromote.ProgressionLevel = ++employeeToPromote.ProgressionLevel;
                employeeToPromote.LastProgressionYear = DateTime.Now.Year;
            }
            CurrentYear.GetInstance.AddYear();
            Console.WriteLine(PromoteToString(employeesToPromote));
        }

        private static void ValidateEmployeesToPromote(IList<Employee> employeesToPromote)
        {
            if (!employeesToPromote.Any())
            {
                throw new Exception(Messages.NoEmployeeToPromote);
            }
        }

        public IList<Employee> GetEmployeesToPromote(int numberEmployeesToPromote)
        {
            var employees = CompanyLocalStorage.GetInstance.GetEmployees();
            var employeesToPromote = employees
                 .Where(employee => employee.ProgressionLevel != 5)
                 .OrderByDescending(PointsToProgression)
                 .Take(numberEmployeesToPromote)
                 .ToList();

            return employeesToPromote;
        }

        private int CompanyTimePoints(Employee employee)
        {
            return (CurrentYear.GetInstance.Year - employee.AdmissionYear) * 2;
        }

        private int TimeWithoutProgressionPoints(Employee employee)
        {
            var timeWithoutProgressionPoints = 0;
            var timeWithoutProgression = CurrentYear.GetInstance.Year - employee.LastProgressionYear;
            if (employee.ProgressionLevel < 4
                || (employee.ProgressionLevel == 4 && timeWithoutProgression >= 2))
            {
                timeWithoutProgressionPoints = 3 * timeWithoutProgression;
            }

            return timeWithoutProgressionPoints;
        }

        private int AgePoint(Employee employee)
        {
            return (CurrentYear.GetInstance.Year - employee.BirthYear) / 5;
        }

        private int PointsToProgression(Employee employee)
        {
            return CompanyTimePoints(employee)
                + TimeWithoutProgressionPoints(employee)
                + AgePoint(employee);
        }

        public string PromoteToString(IList<Employee> employees)
        {
            var toString = "===============PROMOTE=============== \n";
            foreach (var employee in employees)
            {
                toString += $"{employee.Name} - From: {employee.ProgressionLevel - 1} - To: {employee.ProgressionLevel} \n";
            }
            return toString;
        }
    }
}
