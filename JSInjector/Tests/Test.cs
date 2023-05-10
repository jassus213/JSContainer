using System;
using System.Diagnostics;
using JSInjector.Contracts;

namespace JSInjector.Tests
{
    public class Test : IDisposable
    {
        public readonly IContainer DiContainer;
        public readonly IBar Bar;
        public readonly IFoo Foo;
        
        public Test(IBar bar, IFoo foo, IContainer diContainer)
        {
            Bar = bar;
            Foo = foo;
            DiContainer = diContainer;

            Debug.WriteLine($"{GetType().Name} : Bar Guid is {Bar.PrintGUID()}, Foo Guid is {Foo.PrintGUID()}");
        }

        public void Dispose()
        {
        }
    }
}