using System;
using TestProject;

namespace JSInjector.Tests
{
    public class Test
    {
        public void TestConstructor()
        {
            var company = new Company();
            var manager = new Manager(company);
            Console.WriteLine(manager.GetType().GetConstructors());
            
        }
    }
}