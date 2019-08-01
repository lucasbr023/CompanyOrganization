using CompanyOrganization.Enumeration;
using CompanyOrganization.Storage;
using CompanyOrganization.Utils;

namespace CompanyOrganization.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine(Constants.HELP);
            var companyLocalStorage = CompanyLocalStorage.GetInstance;
            companyLocalStorage.CreateCompany();
            var commandString = System.Console.ReadLine();

            while (commandString != null && !commandString.Equals(Constants.COMMAND_EXIT))
            {
                try
                {
                    var command = CommandFactory.GetCommand(commandString);
                    System.Console.WriteLine(command.Execute(commandString));
                }
                catch (System.Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
                finally
                {
                    commandString = System.Console.ReadLine();
                }
            }
        }
    }
}
