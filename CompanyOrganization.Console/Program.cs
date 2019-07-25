using CompanyEmployeesBalancing.Console.Configuration;
using CompanyEmployeesBalancing.Services.Implementation;

namespace CompanyOrganization.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = SimpleInjectorConfigurator.Configure();

            var command = container.GetInstance<CommandService>();
            command.Start();
        }
    }
}
