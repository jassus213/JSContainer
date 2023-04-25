using JSInjector.Binding.BindInfo;
using IContainer = JSInjector.Contracts.IContainer;

namespace JSInjector.Factories
{
    public abstract class Factory : IFactory
    {
        protected virtual void Create<TResult>(IContainer container, TResult result)
        {
            container.Bind<TResult>().FromResolve(result, BindTypes.Default);
        }
    }
}