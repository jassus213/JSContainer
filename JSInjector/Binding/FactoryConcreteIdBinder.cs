using JSInjector.Binding.BindInfo;

namespace JSInjector.Binding
{
    public class FactoryConcreteBinderId<TFactory>
    {
        private readonly DiContainer _diContainer;
        internal readonly FactoryBindInfo FactoryBindInfo;
        
        public FactoryConcreteBinderId(DiContainer diContainer, FactoryBindInfo factoryBindInfo)
        {
            _diContainer = diContainer;
            FactoryBindInfo = factoryBindInfo;
        }
    }
}