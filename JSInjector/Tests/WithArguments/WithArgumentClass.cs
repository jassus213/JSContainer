namespace JSInjector.Tests
{
    public class WithArgumentClass
    {
        public Foo Foo;
        public IBar Bar;
        
        public WithArgumentClass(IBar bar, Foo foo)
        {
            Bar = bar;
            Foo = foo;
        }
    }
}