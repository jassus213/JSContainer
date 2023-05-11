using System.Collections.Generic;
using ConsoleExample.Entity;
using ConsoleExample.Services.Employee.Entity;

namespace ConsoleExample.Services.Common
{
    public interface IJsonReader<TEntity> where TEntity : IJsonEntity
    {
        List<EmployeeEntity> ReadFile();
    }
}