using JSInjector.Contracts;
using JSInjector.Factories;

namespace JSInjector.Tests
{
    public class TestFactory : Factory, IFactory<Test>
    {
        private readonly IContainer _diContainer;
        
        public TestFactory(IContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public Test Create()
        {
            //var result = new Test(new Bar(new DiContainer()), new Foo(new DiContainer()), new DiContainer());
            //Create(_diContainer, result);
            return null;
        }

        protected override void Create<TResult>(IContainer container, TResult result)
        {
            base.Create(container, result);
        }
    }
}