using System.Collections.Generic;
using System.IO;
using ConsoleExample.Entity;
using ConsoleExample.Services.Employee.Entity;
using Newtonsoft.Json;

namespace ConsoleExample.Services.Employee
{
    public class EmployeeWriter : IJsonWriter<EmployeeEntity>
    {
        private readonly IEmployeeReader _employeeReader;

        public EmployeeWriter(IEmployeeReader employeeReader)
        {
            _employeeReader = employeeReader;
        }

        public void WriteToFile(EmployeeEntity entity)
        {
            var employees = _employeeReader.ReadFile();
            if (employees == null)
                employees = new List<EmployeeEntity>();
            
            employees.Add(entity);
            var jsonString = JsonConvert.SerializeObject(employees, Formatting.Indented);
            File.WriteAllText(EmployeeDataInfo.Path, jsonString);
        }
    }
}