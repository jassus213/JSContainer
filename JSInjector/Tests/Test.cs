namespace JSInjector.Tests
{
    public class Test
    {
        public readonly DiContainer DiContainer;
        public readonly IBar Bar;
        
        public Test(IBar bar, DiContainer diContainer)
        {
            Bar = bar;
            DiContainer = diContainer;
        }
    }
}