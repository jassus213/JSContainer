using System;
using ConsoleExample.Services.Common;
using ConsoleExample.Services.Employee.Entity;

namespace ConsoleExample.Services.Employee
{
    public interface IEmployeeReader : IJsonReader<EmployeeEntity>
    {
        EmployeeEntity FindEntity(string predicate);
    }
}