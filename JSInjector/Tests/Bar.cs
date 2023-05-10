using System.Diagnostics;

namespace JSInjector.Tests
{
    public class Bar : IBar
    {
        public readonly DiContainer DiContainer;
        public readonly IFoo Foo;
        private readonly string Guid;

        public Bar(IFoo foo, DiContainer diContainer)
        {
            Foo = foo;
            DiContainer = diContainer;
            Guid = System.Guid.NewGuid().ToString();
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}