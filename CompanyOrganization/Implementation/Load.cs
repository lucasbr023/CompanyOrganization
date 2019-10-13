using CompanyOrganization.Contract;
using CompanyOrganization.Domain.BusinessObjects;
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
        private CompanyLocalStorage _companyLocalStorage;

        public Load()
        {
            _companyLocalStorage = CompanyLocalStorage.GetInstance;
        }

        public string Execute(string parameters)
        {
            string[] filesNames = parameters.Replace("Load ", string.Empty).Split(' ');
            if (filesNames.Length == Constants.NUMBER_ARGUMENTS_LOAD)
            {
                _companyLocalStorage.CreateCompany();

                UpdateEntityWithCsvFile(filesNames);

                var employeeFilePath = GetFilePath(filesNames[Constants.INDEX_EMPLOYEE_FILE].Replace("\"", string.Empty));
                var lines = GetLinesCsvFile(employeeFilePath);
                _companyLocalStorage.UpdateEmployees(lines.Select(line => new Employee(line)).ToList());

                return Messages.FileLoaded;
            }
            return string.Empty;
        }

        private void UpdateEntityWithCsvFile(string[] filesNames)
        {
            var filePath = GetFilePath(filesNames[Constants.INDEX_TEAM_FILE].Replace("\"", string.Empty));
            var lines = GetLinesCsvFile(filePath);
            _companyLocalStorage.UpdateTeams(lines.Select(line => new Team(line)).ToList());
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

        private string GetFilePath(string fileName)
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
