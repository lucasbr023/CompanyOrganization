using System;

namespace CompanyOrganization.Utils
{
    public sealed class CurrentYear
    {

        public int Year { get; set; }

        public CurrentYear()
        {
            Year = DateTime.Now.Year;
        }

        public static CurrentYear _instance;

        private static readonly object _lock = new object();

        public static CurrentYear GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CurrentYear();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddYear()
        {
            Year++;
        }
        
        public void UpdateYear(int year)
        {
            Year = year;
        }
    }
}
