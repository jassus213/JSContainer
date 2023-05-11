using System;
using System.Collections.Generic;
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

            var commands = mainInstaller.Container.ResolveAll<ICommand>();

            var commandsMap = new Dictionary<string, Action>();

            foreach (var command in commands)
            {
                commandsMap.Add(command.CommandName, () => command.Execute());
            }

            while (true)
            {
                var currentCommand = Console.ReadLine();
                if (commandsMap.ContainsKey(currentCommand))
                    commandsMap[currentCommand].Invoke();
            }
        }
    }
}