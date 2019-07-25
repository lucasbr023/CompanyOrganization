using CompanyOrganization.Console.Configuration;
using CompanyOrganization.Implementation;

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
