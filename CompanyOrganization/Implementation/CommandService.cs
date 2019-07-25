using CompanyEmployeesBalancing.Domain;
using CompanyEmployeesBalancing.Resources;
using CompanyEmployeesBalancing.Services.Contract;
using CompanyEmployeesBalancing.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyEmployeesBalancing.Domain.Enumeration;

namespace CompanyEmployeesBalancing.Services.Implementation
{
    public class CommandService : ICommandService
    {
        private readonly IBalanceService _balanceService;
        private readonly IAllocateService _allocateService;
        private readonly ILoadService _loadService;
        private readonly IPromoteService _promoteService;
        private readonly ITeamService _teamService;
        private readonly IEmployeeService _employeeService;

        public CommandService(IBalanceService balanceService, 
                              IAllocateService allocateService, 
                              ILoadService loadService, 
                              IPromoteService promoteService, 
                              ITeamService teamService, 
                              IEmployeeService employeeService)
        {
            _balanceService = balanceService;
            _allocateService = allocateService;
            _loadService = loadService;
            _promoteService = promoteService;
            _teamService = teamService;
            _employeeService = employeeService;
        }

        public void Start()
        {
            Console.WriteLine(Constants.HELP);
            var companyLocalStorage = CompanyLocalStorage.GetInstance;
            companyLocalStorage.CreateCompany();
            var commandString = Console.ReadLine();

            while (commandString != null && !commandString.Equals(Constants.COMMAND_EXIT))
            {
                try
                {
                    var command = GetCommand(commandString);
                    ExecuteCommand(command);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                finally
                {
                    commandString = Console.ReadLine();
                }
            }
        }

        public void ExecuteCommand(CommandDto command)
        {
            switch (command.Command)
            {
                case CommandEnum.Load:
                    Load(command.Arguments);
                    break;
                case CommandEnum.Allocate:
                    Allocate();
                    break;
                case CommandEnum.Promote:
                    Promote(command.Arguments);
                    break;
                case CommandEnum.Balance:
                    Balance();
                    break;
                default:
                    throw new Exception(Messages.InvalidCommand);
            }
        }

        #region GetCommand
        public CommandDto GetCommand(string commandAction)
        {
            var commandDto = new CommandDto();
            var commandActionSplit = commandAction.Split(Constants.BLANK_SPACE).ToList();

            if (commandAction.ToLower().Contains(Constants.COMMAND_LOAD))
            {
                GetCommandLoad(commandDto, commandActionSplit);
            }
            else if (commandAction.ToLower().Contains(Constants.COMMAND_ALLOCATE))
            {
                commandDto.Command = CommandEnum.Allocate;
            }
            else if (commandAction.ToLower().Contains(Constants.COMMAND_PROMOTE))
            {
                GetCommandPromote(commandDto, commandActionSplit);
            }
            else if (commandAction.ToLower().Contains(Constants.COMMAND_BALANCE))
            {
                commandDto.Command = CommandEnum.Balance;
            }
            else
            {
                throw new Exception(Messages.InvalidCommand);
            }
            return commandDto;
        }

        private void GetCommandLoad(CommandDto commandDto, IList<string> commandActionSplit)
        {
            ValidateArgumentsLoadFile(commandActionSplit);
           
            commandDto.Command = CommandEnum.Load;
            commandDto.Arguments.AddRange(new List<string>()
            {
                commandActionSplit[Constants.INDEX_ARGUMENTS_TEAM_FILE],
                commandActionSplit[Constants.INDEX_ARGUMENTS_EMPLOYEE_FILE]
            });
        }

        private void GetCommandPromote(CommandDto commandDto, IList<string> commandActionSplit)
        {
            commandDto.Command = CommandEnum.Promote;
            ValidateArgumentsPromote(commandActionSplit);
            ValidatePromoteArgumentValue(commandActionSplit);

            commandDto.Arguments.AddRange(new List<string>()
            {
                commandActionSplit[Constants.INDEX_ARGUMENTS_PROMOTE]
            });
        }

        private void ValidatePromoteArgumentValue(IList<string> commandActionSplit)
        {
            var value = commandActionSplit.FirstOrDefault(command => !command.ToLower().Equals(Constants.COMMAND_PROMOTE));
            GetValueFromCommandValue(value);
        }

        private void ValidateArgumentsLoadFile(IList<string> commandActionSplit)
        {
            if (commandActionSplit.ElementAtOrDefault(Constants.INDEX_ARGUMENTS_TEAM_FILE) == null
                || commandActionSplit.ElementAtOrDefault(Constants.INDEX_ARGUMENTS_EMPLOYEE_FILE) == null)
            {
                throw new Exception(Messages.InvalidFilesToLoad);
            }
        }
        
        private void ValidateArgumentsPromote(IList<string> commandActionSplit)
        {
            if (commandActionSplit.ElementAtOrDefault(Constants.INDEX_ARGUMENTS_PROMOTE) == null)
            {
                throw new Exception(Messages.NumberEmployeePromoteNotInformed);
            }
        }

        #endregion

        #region ExecuteCommand
        private void Load(IList<string> filesNames)
        {
            if (filesNames.Count == Constants.NUMBER_ARGUMENTS_LOAD)
            {
                var companyLocalStorage = CompanyLocalStorage.GetInstance;

                var teamFilePath =
                    _loadService.GetFilePath(filesNames[Constants.INDEX_TEAM_FILE].Replace("\"", string.Empty));
                var employeeFilePath =
                    _loadService.GetFilePath(filesNames[Constants.INDEX_EMPLOYEE_FILE].Replace("\"", string.Empty));
                companyLocalStorage
                    .UpdateTeams(_teamService.CreateTeams(_loadService.GetLinesCsvFile(teamFilePath)));
                companyLocalStorage
                    .UpdateEmployees(_employeeService.CreateEmployees(_loadService.GetLinesCsvFile(employeeFilePath)));

                Console.WriteLine(Messages.FileLoaded);
            }
        }

        private void Allocate()
        {
            var companyLocalStorage = CompanyLocalStorage.GetInstance;
            _allocateService.Allocate(companyLocalStorage.GetTeams(), companyLocalStorage.GetEmployees());
            Console.WriteLine(_allocateService.AllocateToString(CompanyLocalStorage.GetInstance.GetCompany()));
        }

        private void Promote(IList<string> arguments)
        {
            var numberEmployeesToPromote = GetValueFromCommandValue(arguments.First());
            _promoteService.Promote(numberEmployeesToPromote);
        }

        private void Balance()
        {
            _balanceService.Balance();
            Console.WriteLine(_balanceService.BalanceToString(CompanyLocalStorage.GetInstance.GetCompany()));
        }
        #endregion
        
        private int GetValueFromCommandValue(string commandValue)
        {
            if (!int.TryParse(commandValue, out var value))
            {
                throw new Exception(Messages.InvalidValue);
            }
            return value;
        }
    }
}
