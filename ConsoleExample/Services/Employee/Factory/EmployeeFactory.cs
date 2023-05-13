using System;
using ConsoleExample.Services.Employee.Entity;

namespace ConsoleExample.Services.Employee.Factory
{
    public static class EmployeeFactory
    {
        public static EmployeeEntity Create(string name, string lastName)
        {
            return new EmployeeEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                LastName = lastName
            };
        }
    }
}