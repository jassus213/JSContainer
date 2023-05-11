using System;
using ConsoleExample.Entity;

namespace ConsoleExample.Services.Employee.Entity
{
    public class EmployeeEntity : IJsonEntity
    {
        public string Id => Guid.NewGuid().ToString();
        public string Name;
        public string LastName;
    }
}