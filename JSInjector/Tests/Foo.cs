using JSInjector;
using JSInjector.Tests;

namespace NUnitTests
{
    public class Foo
    {
        public readonly DiContainer DiContainer;

        public Foo(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }
    }
}