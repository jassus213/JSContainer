namespace JSInjector.Tests
{
    public class Test
    {
        public readonly DiContainer DiContainer;
        public readonly Bar Bar;
        
        public Test(Bar bar, DiContainer diContainer)
        {
            Bar = bar;
            DiContainer = diContainer;
        }
    }
}