namespace CompanyOrganization.Utils
{
    public class Constants
    {
        public const string COMMAND_EXIT = "exit";
        public const string COMMAND_LOAD = "load";
        public const string COMMAND_ALLOCATE = "allocate";
        public const string COMMAND_PROMOTE = "promote";
        public const string COMMAND_BALANCE = "balance";

        public const int NUMBER_ARGUMENTS_LOAD = 2;

        public const int INDEX_NAME_EMPLOYEE = 0;
        public const int INDEX_PROGRESSION_LEVEL_EMPLOYEE = 1;
        public const int INDEX_BIRTH_YEAR_EMPLOYEE = 2;
        public const int INDEX_ADMISSION_YEAR_EMPLOYEE = 3;
        public const int INDEX_LAST_PROGRESSION_YEAR_EMPLOYEE = 4;

        public const int INDEX_TEAM_FILE = 0;
        public const int INDEX_EMPLOYEE_FILE = 1;

        public const int INDEX_ARGUMENTS_TEAM_FILE = 1;
        public const int INDEX_ARGUMENTS_EMPLOYEE_FILE = 2;
        public const int INDEX_ARGUMENTS_PROMOTE = 1;

        public const int INDEX_NAME_TEAM = 0;
        public const int INDEX_MINIMUM_MATURITY_TEAM = 1;

        public const char SEMICOLON = ';';

        public const string CSV_FILE_EXTENSION = ".csv";

        public const char BLANK_SPACE = ' ';


        public const string HELP = @"
==============HELP=========================================
load team_file employees_file - files must be on the following folder: 
    '..\companyorganization\CompanyOrganization\Files'

allocate - allocates employees to teams in the company

promote number_employees_to_promote - promotes employees according to the criteria

balance - balance teams for optimal scenario.

exit - exits program
================================
example of files: team.csv, employees.csv
example of command:
load team.csv employees.csv
allocate
promote 2
balance
==========================================================";
    }
}
