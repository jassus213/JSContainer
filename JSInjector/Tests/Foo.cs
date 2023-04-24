using System;

namespace JSInjector.Tests
{
    public class Foo : IFoo
    {
        public readonly DiContainer DiContainer;

        public Foo(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }

        public void Test()
        {
            Console.WriteLine("Test");
        }
    }
}