using JSInjector.Binding;

namespace JSInjector.Contracts
{
    public interface IContainer
    {
        TContract Resolve<TContract>();
        ConcreteIdBinder<TContract> Bind<TContract>();
        void BindInterfacesTo<TContract>();
        void BindSelfTo<TContract>();
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>();
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>();

    }
}