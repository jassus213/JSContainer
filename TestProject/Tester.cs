using System;

namespace TestProject
{
    public class Tester : ITester
    {
        private readonly Company _company;
        private readonly Manager _manager;
        public Tester(DiContainer container)
        {
            _company = company;
            Console.WriteLine(company.Id);
            CreateManager();
        }
        public void CreateManager()
        {
            Console.WriteLine($"Manager injected with ID {_manager.Id}");
        }
    }
}