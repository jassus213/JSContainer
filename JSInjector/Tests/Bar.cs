namespace JSInjector.Tests
{
    public class Bar : IBar
    {
        public readonly DiContainer DiContainer;

        public Bar(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }
    }
}