namespace TestFramework.Entity
{
    public class AnotherScope2
    {
        public readonly IFoo Foo;
        
        public AnotherScope2(IFoo foo)
        {
            Foo = foo;
        }
    }
}