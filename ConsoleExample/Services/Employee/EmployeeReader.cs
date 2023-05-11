using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleExample.Entity;
using ConsoleExample.Services.Employee.Entity;
using Newtonsoft.Json;

namespace ConsoleExample.Services.Employee
{
    public class EmployeeReader : IEmployeeReader
    {
        public EmployeeEntity FindEntityById(int id)
        {
            var employees = ReadFile();
            return employees.First();
        }

        public List<EmployeeEntity> ReadFile()
        {
            var initString = File.ReadAllText(EmployeeDataInfo.Path);
            
            if (initString == String.Empty)
                return null;
            
            var jsonString = JsonConvert.DeserializeObject<List<EmployeeEntity>>(initString);
            return jsonString.ToList();
        }
    }
}