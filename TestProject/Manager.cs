using System;
using System.IO;

namespace TestProject
{
    public class Manager
    {
        public string Id { get; set; }
        public Company Company { get; set; }
        public Tester Tester { get; set; }

        public Manager(Company company, Tester tester)
        {
            Id = new Guid().ToString();
            Company = company;
            Tester = tester;
            Console.WriteLine(Company.Id);
        }
        
    }
}