using System;
using ConsoleExample.Entity;
using ConsoleExample.Services;
using ConsoleExample.Services.Employee.Entity;
using ICommand = ConsoleExample.Commands.Common.ICommand;

namespace ConsoleExample.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        public string CommandName => _command;
        private readonly string _command = "Add Employee";

        private readonly IJsonWriter<EmployeeEntity> _jsonWriter;

        public AddEmployeeCommand(IJsonWriter<EmployeeEntity> jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        public void Execute()
        {
            var name = Console.ReadLine();
            var lastName = Console.ReadLine();

            var entity = new EmployeeEntity
            {
                Name = name,
                LastName = lastName,
            };

            _jsonWriter.WriteToFile(entity);
        }
    }
}