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
            var installer = new StartInstaller();
            installer.Install();
        }
    }
}