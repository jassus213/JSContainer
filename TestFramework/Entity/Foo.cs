namespace TestFramework.Entity
{
    public class Foo : IFoo
    {
        private readonly string Guid;

        public Foo()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public string PrintGUID()
        {
            return Guid;
        }
    }
}