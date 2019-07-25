using CompanyOrganization.Contract;
using CompanyOrganization.Implementation;
using CompanyOrganization.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;

namespace CompanyEmployeesBalancing.Test
{
    [TestClass]
    public class Setup
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
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

            DependencyResolver.SetupContainer(container);
        }
    }
}
