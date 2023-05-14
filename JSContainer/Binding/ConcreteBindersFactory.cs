using System;
using System.Linq.Expressions;
using JSContainer.Binding.BindInfo;
using JSContainer.Utils.Instance;

namespace JSContainer.Binding
{
    internal static class ConcreteBindersFactory
    {
        internal static ConcreteIdBinder<TConcrete> Create<TConcrete>(DiContainer container,
            BindInformation bindInformation)
        {
            var parameters = InstanceUtil.ParametersUtil.Map(new[] { container.GetType(), bindInformation.GetType() });
            var body = Expression.New(InstanceUtil.ConstructorUtils.GetConstructor(typeof(ConcreteIdBinder<TConcrete>),
                ConstructorConventionsSequence.First), parameters);
            return Expression.Lambda<Func<DiContainer, BindInformation, ConcreteIdBinder<TConcrete>>>(body, parameters)
                .Compile().Invoke(container, bindInformation);
        }

        internal static ConcreteIdLifeCycle<TConcrete> Create<TConcrete>(DiContainer container)
        {
            var parameters = InstanceUtil.ParametersUtil.Map(new[] { container.GetType()});
            var body = Expression.New(InstanceUtil.ConstructorUtils.GetConstructor(typeof(ConcreteIdLifeCycle<TConcrete>),
                ConstructorConventionsSequence.First), parameters);
            return Expression.Lambda<Func<DiContainer, ConcreteIdLifeCycle<TConcrete>>>(body, parameters)
                .Compile().Invoke(container);
        }
    }
}