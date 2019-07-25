using CompanyOrganization.Contract;
using CompanyOrganization.Domain.BusinessObjects;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        public List<Employee> CreateEmployees(IList<string> employeesLines)
        {
            var employees = new List<Employee>();
            
            foreach (var line in employeesLines)
            {
                var splitLine = line.Split(Constants.SEMICOLON).ToList();
                ValidateEmployeeLine(splitLine);

                employees.Add(new Employee()
                {
                    Name = splitLine[Constants.INDEX_NAME_EMPLOYEE],
                    ProgressionLevel = Util.ConvertStringToInt(splitLine[Constants.INDEX_PROGRESSION_LEVEL_EMPLOYEE]),
                    BirthYear = Util.ConvertStringToInt(splitLine[Constants.INDEX_BIRTH_YEAR_EMPLOYEE]),
                    AdmissionYear = Util.ConvertStringToInt(splitLine[Constants.INDEX_ADMISSION_YEAR_EMPLOYEE]),
                    LastProgressionYear = !string.IsNullOrEmpty(splitLine[Constants.INDEX_LAST_PROGRESSION_YEAR_EMPLOYEE]) ? 
                                          Util.ConvertStringToInt(splitLine[Constants.INDEX_LAST_PROGRESSION_YEAR_EMPLOYEE]) :
                                          Util.ConvertStringToInt(splitLine[Constants.INDEX_ADMISSION_YEAR_EMPLOYEE])
                });
            }
            return employees;
        }

        private void ValidateEmployeeLine(IList<string> splitLine)
        {
            if (splitLine.ElementAtOrDefault(Constants.INDEX_NAME_EMPLOYEE) == null
                || splitLine.ElementAtOrDefault(Constants.INDEX_PROGRESSION_LEVEL_EMPLOYEE) == null
                || splitLine.ElementAtOrDefault(Constants.INDEX_BIRTH_YEAR_EMPLOYEE) == null
                || splitLine.ElementAtOrDefault(Constants.INDEX_ADMISSION_YEAR_EMPLOYEE) == null)
            {
                throw new Exception(string.Format(Messages.FileWithInvalidValues, "Employee"));
            }
        }
    }
}
