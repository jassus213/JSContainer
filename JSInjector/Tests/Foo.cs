namespace JSInjector.Tests
{
    public class Foo : IFoo
    {
        public readonly DiContainer DiContainer;
        public readonly Test Test;

        public Foo(DiContainer diContainer, Test test)
        {
            DiContainer = diContainer;
            Test = test;
        }
    }
}