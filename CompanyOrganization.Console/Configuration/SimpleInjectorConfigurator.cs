using CompanyOrganization.Contract;
using CompanyOrganization.Implementation;
using SimpleInjector;

namespace CompanyOrganization.Console.Configuration
{
    public class SimpleInjectorConfigurator
    {
        public static Container Configure()
        {
            var container = new Container();

            container.Register<ILoadService, LoadService>(Lifestyle.Singleton);
            container.Register<ICommandService, CommandService>(Lifestyle.Singleton);
            container.Register<IAllocateService, AllocateService>(Lifestyle.Singleton);
            container.Register<IBalanceService, BalanceService>(Lifestyle.Singleton);
            container.Register<IEmployeeService, EmployeeService>(Lifestyle.Singleton);
            container.Register<ITeamService, TeamService>(Lifestyle.Singleton);
            container.Register<IPromoteService, PromoteService>(Lifestyle.Singleton);
            container.Verify();
            
            return container;
        }   
    }
}
