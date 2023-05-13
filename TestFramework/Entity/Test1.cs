namespace TestFramework.Entity
{
    public class Test1
    {
        public readonly TestScope1 TestScope1;
        public readonly IFoo Foo;

        public Test1(IFoo foo, TestScope1 testScope1)
        {
            Foo = foo;
            TestScope1 = testScope1;
        }
    }
}