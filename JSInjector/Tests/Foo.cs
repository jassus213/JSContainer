namespace JSInjector.Tests
{
    public class Foo : IFoo
    {
        public readonly DiContainer DiContainer;
        private readonly string Guid;

        public Foo(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}