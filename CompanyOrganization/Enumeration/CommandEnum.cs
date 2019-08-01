using CompanyOrganization.Contract;
using CompanyOrganization.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyOrganization.Enumeration
{
    public enum CommandEnum
    {
        Load,
        Allocate,
        Promote,
        Balance
    }

    public static class CommandFactory
    {
        private static readonly Dictionary<CommandEnum, ICommand> Commands = new Dictionary<CommandEnum, ICommand>() {
            [CommandEnum.Load] = new Load(),
            [CommandEnum.Allocate] = new Allocate(),
            [CommandEnum.Promote] = new Promote(),
            [CommandEnum.Balance] = new Balance()
        };

        public static ICommand GetCommand(string command)
        {
            try
            {
                var commandEnum = (CommandEnum)Enum.Parse(typeof(CommandEnum), command.Split(' ').First());
                return Commands[commandEnum];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
