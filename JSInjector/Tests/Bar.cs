using System.Diagnostics;

namespace JSInjector.Tests
{
    public class Bar : IBar
    {
        public readonly DiContainer DiContainer;
        private readonly string Guid;

        public Bar(DiContainer diContainer)
        {
            DiContainer = diContainer;
            Guid = System.Guid.NewGuid().ToString();
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}