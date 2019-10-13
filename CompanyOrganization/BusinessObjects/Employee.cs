using CompanyOrganization.Utils;
using System;

namespace CompanyOrganization.Domain.BusinessObjects
{
    public class Employee
    {
        public string Name { get; set; }

        public int BirthYear { get; set; }

        public int AdmissionYear { get; set; }

        public int LastProgressionYear { get; set; }

        public int ProgressionLevel { get; set; }

        public Employee(string line)
        {
            try
            {
                var split = line.Split(Constants.SEMICOLON);
                Name = split[Constants.INDEX_NAME_EMPLOYEE];
                ProgressionLevel = Util.ConvertStringToInt(split[Constants.INDEX_PROGRESSION_LEVEL_EMPLOYEE]);
                BirthYear = Util.ConvertStringToInt(split[Constants.INDEX_BIRTH_YEAR_EMPLOYEE]);
                AdmissionYear = Util.ConvertStringToInt(split[Constants.INDEX_ADMISSION_YEAR_EMPLOYEE]);
                LastProgressionYear = !string.IsNullOrEmpty(split[Constants.INDEX_LAST_PROGRESSION_YEAR_EMPLOYEE]) ?
                                              Util.ConvertStringToInt(split[Constants.INDEX_LAST_PROGRESSION_YEAR_EMPLOYEE]) :
                                              Util.ConvertStringToInt(split[Constants.INDEX_ADMISSION_YEAR_EMPLOYEE]);
            }
            catch (Exception)
            {

                throw new Exception(string.Format(Messages.FileWithInvalidValues, "Employee"));
            }
        }
    }
}
