using System;

namespace TestProject
{
    public class InjectProvider
    {
        private string Id { get; set; }
        
        public InjectProvider()
        {
            Id = Guid.NewGuid().ToString();
            Injected();
        }

        public void Injected()
        {
            Console.WriteLine($"Inject provider created with id {Id}");
        }
    }
}