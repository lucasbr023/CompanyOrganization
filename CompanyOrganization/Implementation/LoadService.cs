using CompanyOrganization.Contract;
using CompanyOrganization.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompanyOrganization.Implementation
{
    public class LoadService : ILoadService
    {
        public IList<string> GetLinesCsvFile(string filePath)
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
            return AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Files\" +  fileName;
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
