using System;

namespace TestProject
{
    public class Company
    {
        public string Id { get; set; }
        public Manager Manager { get; set; }
        
        public Company(Manager manager)
        {
            Id = Guid.NewGuid().ToString();
            Injected();
            Manager = manager;
        }

        private void Injected()
        {
            Console.WriteLine($"Manager injected with ID {Manager.Id}");
        }

    }
}