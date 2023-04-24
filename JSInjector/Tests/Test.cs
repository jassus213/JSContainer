namespace JSInjector.Tests
{
    public class Test
    {
        public readonly DiContainer DiContainer;
        public readonly IBar Bar;
        public readonly IFoo Foo;
        
        public Test(IBar bar, IFoo foo, DiContainer diContainer)
        {
            Bar = bar;
            Foo = foo;
            DiContainer = diContainer;
        }
    }
}