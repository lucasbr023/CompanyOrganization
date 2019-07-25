using CompanyEmployeesBalancing.Domain;

namespace CompanyEmployeesBalancing.Services.Contract
{
    public interface ICommandService
    {
        /// <summary>
        /// Start the command
        /// </summary>
        void Start();

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="command">command information</param>
        /// <returns></returns>
        void ExecuteCommand(CommandDto command);

        /// <summary>
        /// Get dto of command
        /// </summary>
        /// <param name="commandAction">command action</param>
        /// <returns>DTO of command</returns>
        CommandDto GetCommand(string commandAction);
    }
}
