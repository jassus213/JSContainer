using System;
using System.Diagnostics;

namespace JSInjector.Tests
{
    public class Foo : IFoo
    {
        public readonly DiContainer DiContainer;
        private readonly string Guid;

        public Foo(DiContainer diContainer)
        {
            DiContainer = diContainer;
            Guid = System.Guid.NewGuid().ToString();
        }

        public void Test()
        {
            Console.WriteLine("Test");
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}