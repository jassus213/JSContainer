namespace TestFramework.Entity
{
    public class Bar : IBar
    {
        public readonly IFoo Foo;
        private readonly string Guid;

        public Bar(IFoo foo)
        {
            Foo = foo;
            Guid = System.Guid.NewGuid().ToString();
            Foo.PrintGUID();
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}