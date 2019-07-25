using System;

namespace CompanyEmployeesBalancing.Resources
{
    public static class Util
    {
        public static int ConvertStringToInt(string stringValue)
        {
            int value;
            try
            {
                value = Int32.Parse(stringValue);
            }
            catch (Exception)
            {
                throw new Exception(Messages.InvalidValue);
            }
            return value;
        }
    }
}
