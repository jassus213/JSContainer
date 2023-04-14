using System;

namespace TestProject
{
    public class Company
    {
        public string Id { get; set; }
        
        public Company()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}