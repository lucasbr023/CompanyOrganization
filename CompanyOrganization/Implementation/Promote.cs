using CompanyOrganization.Contract;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Storage;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class Promote : ICommand
    {
        public string Execute(string parameters = null)
        {
            var parameter = parameters.Split(' ');
           
            ValidateArgumentsPromote(parameter);
            ValidatePromoteArgumentValue(parameter);

            var employeesToPromote = GetEmployeesToPromote(GetValueFromCommandValue(parameter.Last()));

            ValidateEmployeesToPromote(employeesToPromote);

            foreach (var employeeToPromote in employeesToPromote)
            {
                employeeToPromote.ProgressionLevel = ++employeeToPromote.ProgressionLevel;
                employeeToPromote.LastProgressionYear = DateTime.Now.Year;
            }
            CurrentYear.GetInstance.AddYear();
            return ToString(employeesToPromote);
        }

        private void ValidateArgumentsPromote(IList<string> commandActionSplit)
        {
            if (commandActionSplit.ElementAtOrDefault(Constants.INDEX_ARGUMENTS_PROMOTE) == null)
            {
                throw new Exception(Messages.NumberEmployeePromoteNotInformed);
            }
        }

        private void ValidatePromoteArgumentValue(IList<string> commandActionSplit)
        {
            var value = commandActionSplit.FirstOrDefault(command => !command.ToLower().Equals(Constants.COMMAND_PROMOTE));
            GetValueFromCommandValue(value);
        }

        private int GetValueFromCommandValue(string commandValue)
        {
            if (!int.TryParse(commandValue, out var value))
            {
                throw new Exception(Messages.InvalidValue);
            }
            return value;
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

        private string ToString(IList<Employee> employees)
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