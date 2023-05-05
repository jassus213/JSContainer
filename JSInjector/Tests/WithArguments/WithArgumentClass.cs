namespace JSInjector.Tests.WithArguments
{
    public class WithArgumentClass
    {
        public Foo Foo;
        public Bar Bar;
        
        public WithArgumentClass(Bar bar, Foo foo)
        {
            Bar = bar;
            Foo = foo;
        }
    }
}