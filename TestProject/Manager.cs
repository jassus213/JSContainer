using System;

namespace TestProject
{
    public class Manager
    {
        public int Id { get; set; }
        public Company Company { get; set; }

        public Manager(Company company)
        {
            Company = company;
        }

        public Manager()
        {
            
        }
    }
}