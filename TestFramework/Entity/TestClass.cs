using System.Diagnostics;
using JSContainer.Contracts;

namespace TestFramework.Entity
{
    public class TestClass
    {
        public readonly IContainer DiContainer;
        public readonly IFoo Foo;
        public readonly AnotherScope AnotherScope;

        public TestClass(IFoo foo, IContainer diContainer, AnotherScope anotherScope)
        {
            Foo = foo;
            DiContainer = diContainer;
            AnotherScope = anotherScope;

            Debug.WriteLine($"{GetType().Name}, Foo Guid is {Foo.PrintGUID()}");
        }
    }
}