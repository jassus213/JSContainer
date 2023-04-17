namespace JSInjector.Tests
{
    public class Bar
    {
        private readonly Foo _foo;
        
        public Bar(Foo foo)
        {
            _foo = foo;
        }
    }
}