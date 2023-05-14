using JSContainer.Binding;
using JSContainer.Factories;
using JSContainer.Binding.BindInfo;

namespace JSContainer.Contracts
{
    public interface IContainer
    {
        TContract Resolve<TContract>();
        ConcreteIdBinder<TContract> Bind<TContract>();
        ConcreteIdBinder<TContract> BindInterfacesTo<TContract>();
        ConcreteIdBinder<TContract> BindSelfTo<TContract>();
        ConcreteIdBinder<TContract> BindInterfacesAndSelfTo<TContract>();
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory;
        FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory where TArgs : struct;
        void Initialize();
        object InitializeWithOutOrder<TConcrete>();

    }
}