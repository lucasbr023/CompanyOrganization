﻿namespace CompanyEmployeesBalancing.Domain.BusinessObjects
{
    public class Employee
    {
        public string Name { get; set; }

        public int BirthYear { get; set; }

        public int AdmissionYear { get; set; }

        public int LastProgressionYear { get; set; }

        public int ProgressionLevel { get; set; }
    }
}
