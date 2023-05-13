using System;
using ConsoleExample.Commands.Common;
using ConsoleExample.Services.Employee;

namespace ConsoleExample.Commands
{
    public class FindEmployeeCommand : ICommand
    {
        public string CommandName => "Find Employee";
        private readonly IEmployeeReader _employeeReader;

        public FindEmployeeCommand(IEmployeeReader employeeReader)
        {
            _employeeReader = employeeReader;
        }

        public void Execute()
        {
            var predicate = Console.ReadLine();
            var employeeEntity = _employeeReader.FindEntity(predicate);
            if (employeeEntity != null)
                Console.WriteLine($"{employeeEntity.Name} {employeeEntity.LastName}");
        }
    }
}