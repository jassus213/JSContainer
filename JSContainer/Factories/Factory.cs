using JSContainer.Common.Enums;
using JSContainer.Binding.BindInfo;
using IContainer = JSContainer.Contracts.IContainer;

namespace JSContainer.Factories
{
    public abstract class Factory : IFactory
    {
        protected virtual void Create<TResult>(Contracts.IContainer container, TResult result)
        {
            container.Bind<TResult>().FromResolve(result, BindType.Default);
        }
    }
}