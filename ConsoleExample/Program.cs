using System;
using System.Linq;
using ConsoleExample.Services.Installers;
using ICommand = ConsoleExample.Commands.Common.ICommand;

namespace ConsoleExample
{
    internal class Program
    {
        public static void Main()
        {
            var mainInstaller = new MainInstaller();
            mainInstaller.Install();
            
            mainInstaller.Container.Initialize();

            var commands = mainInstaller.Container.ResolveAll<ICommand>();

            var commandsMap = commands.ToDictionary<ICommand, string, Action>(command => command.CommandName, command => command.Execute);

            while (true)
            {
                var currentCommand = Console.ReadLine();
                if (commandsMap.ContainsKey(currentCommand))
                    commandsMap[currentCommand].Invoke();
            }
        }
    }
}