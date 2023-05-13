using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleExample.Services.Employee.Entity;
using Newtonsoft.Json;

namespace ConsoleExample.Services.Employee
{
    public class EmployeeReader : IEmployeeReader
    {
        public EmployeeEntity FindEntity(string predicate)
        {
            var employees = ReadFile();
            var employeeEntity = employees.FindEntity(x => x == predicate);
            return employeeEntity;
        }

        public List<EmployeeEntity> ReadFile()
        {
            var initString = File.ReadAllText(EmployeeJsonSettings.Path);
            
            if (initString == String.Empty)
                return null;
            
            var jsonString = JsonConvert.DeserializeObject<List<EmployeeEntity>>(initString);
            return jsonString.ToList();
        }
    }

    enum EmployeeSequence
    {
        Name,
        LastName,
        Id
    }

    static class EmployeeExtension
    {
        public static EmployeeEntity FindEntity(this IEnumerable<EmployeeEntity> employees, Predicate<string> match, EmployeeSequence sequence = default)
        {
            switch (sequence)
            {
                case EmployeeSequence.Id:
                    return employees.First(entity => match(entity.Id));
                case EmployeeSequence.Name:
                    return employees.First(entity => match(entity.Name));
                case EmployeeSequence.LastName:
                    return employees.First(entity => match(entity.LastName));
                default:
                    throw new NullReferenceException($"Employee Sequence is null");
            }
        }

        public static IEnumerable<TResult> FindEntityNew<TResult, TSource>(this IEnumerable<TSource> sourceEnumerable,
            Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            
            foreach (var source in sourceEnumerable)
            {
                var selectorResult = selector(source);
                if (selectorResult != null)
                    result.Add(selectorResult);
            }

            return result.ToArray();
        }
    }
}