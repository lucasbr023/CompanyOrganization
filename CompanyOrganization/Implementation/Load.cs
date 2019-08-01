using CompanyOrganization.Contract;
using CompanyOrganization.Storage;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class Load : ICommand
    {
        private TeamService _teamService;
        private EmployeeService _employeeService;

        public Load()
        {
            _teamService = new TeamService();
            _employeeService = new EmployeeService();
        }

        public string Execute(string parameters)
        {
            string[] filesNames = parameters.Replace("Load ", string.Empty).Split(' ');
            if (filesNames.Length == Constants.NUMBER_ARGUMENTS_LOAD)
            {
                var companyLocalStorage = CompanyLocalStorage.GetInstance;

                var teamFilePath = GetFilePath(filesNames[Constants.INDEX_TEAM_FILE].Replace("\"", string.Empty));
                var employeeFilePath = GetFilePath(filesNames[Constants.INDEX_EMPLOYEE_FILE].Replace("\"", string.Empty));

                companyLocalStorage.CreateCompany();
                companyLocalStorage.UpdateTeams(_teamService.CreateTeams(GetLinesCsvFile(teamFilePath)));
                companyLocalStorage.UpdateEmployees(_employeeService.CreateEmployees(GetLinesCsvFile(employeeFilePath)));

                return Messages.FileLoaded;
            }
            return string.Empty;
        }

        private IList<string> GetLinesCsvFile(string filePath)
        {
            ValidateFileExtension(filePath);
            var lines = new List<string>();
            try
            {
                lines = File.ReadAllLines(filePath).ToList();
                lines = lines.Skip(1).ToList();
            }
            catch (Exception)
            {
                throw new Exception(Messages.FileNotFound);
            }
            return lines;
        }

        public string GetFilePath(string fileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Files\" + fileName;
        }

        private void ValidateFileExtension(string fileName)
        {
            if (!fileName.ToLower().Contains(Constants.CSV_FILE_EXTENSION))
            {
                throw new Exception(Messages.FileInvalidExtenssion);
            }
        }
    }
}
