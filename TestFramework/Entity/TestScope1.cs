namespace TestFramework.Entity
{
    public class TestScope1
    {
        public IFoo Foo;
        
        public TestScope1(IFoo foo)
        {
            Foo = foo;
        }
    }
}