using JSInjector.Binding;
using JSInjector.Factories;

namespace JSInjector.Contracts
{
    public interface IContainer
    {
        TContract Resolve<TContract>();
        ConcreteIdBinder<TContract> Bind<TContract>();
        void BindInterfacesTo<TContract>();
        void BindSelfTo<TContract>();
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory;
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory where TArgs : struct;

    }
}