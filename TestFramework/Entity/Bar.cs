namespace TestFramework.Entity
{
    public class Bar : IBar
    {
        public readonly IFoo Foo;
        private readonly string Guid;
        private readonly Test1 _test1;

        public Bar(Test1 test1, IFoo foo)
        {
            _test1 = test1;
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