using System;
using System.Collections.Generic;
using System.Reflection;
using JSInjector;
using TestProject;


namespace Progrma
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = new DiContainer();
            var manager = new Manager(new Company());
            var testTypes = new List<Type>();
            testTypes.Add(new Company().GetType());
            container.Bind<Tester>();
            container.Bind<Manager>();
            container.Bind<Company>().To<Manager>();
            container.Instantiate<Manager>();
        }
    }
}