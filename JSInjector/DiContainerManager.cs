using System;
using System.Collections.Generic;
using System.Linq;
using JSInjector.Binding.BindInfo;
using JSInjector.Common.Enums;
using JSInjector.JSExceptions;
using JSInjector.Service;
using JSInjector.Utils;
using JSInjector.Utils.Instance;
using JSInjector.Utils.LifeCycle;

namespace JSInjector
{
    internal static class DiContainerManager
    {
        internal static object SearchInstance<TInstance, TConcrete>(DiContainer container)
            where TConcrete : class where TInstance : class
        {
            var type = typeof(TInstance);
            var typeConcrete = typeof(TConcrete);
            var currentType = type;
            BindInformation bindInformation = null;


            if (InstanceUtil.IsInterfaceAndBinded(currentType, ref container.ContractsInfo, ref container.BindInfoMap))
            {
                currentType = container.ContractsInfo[type].Last();

                bindInformation = container.BindInfoMap[currentType];

                if (!bindInformation.ContractsTypes.Contains(type))
                    throw JsExceptions.BindException.ContractNotBindedToInstance(type, currentType);
            }
            else if (container.BindInfoMap.ContainsKey(currentType))
            {
                bindInformation = container.BindInfoMap[currentType];
            }


            if (container.BindInfoMap[typeConcrete].ArgumentsMap
                .ContainsKey(currentType)) // Get argument from TConcrete
            {
                bindInformation = container.BindInfoMap[typeConcrete];
                var instance = bindInformation.ArgumentsMap[currentType];
                return instance;
            }
            

            if (InstanceUtil.ParametersUtil.HasCircularDependency(container.ContractsInfo, type,
                    InstanceUtil.ParametersUtil.GetParametersExpression(currentType)))
                return null;

            if (bindInformation!.LifeCycle == LifeCycle.Singleton &&
                container.IsSingletonInstanced(currentType))
            {
                return container.ContainerInfo[currentType].Value.Instance;
            }

            if (bindInformation!.LifeCycle == LifeCycle.Singleton &&
                !container.IsSingletonInstanced(currentType))
            {
                var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                var instance = genericMethod.Invoke(null,
                    new object[]
                    {
                        InstanceUtil.ConstructorUtils.GetConstructor(currentType, ConstructorConventionsSequence.First),
                        container
                    });
                return instance;
            }


            if (bindInformation!.LifeCycle == LifeCycle.Scoped &&
                LifeCycleUtil.IsScopedInstanced(ref container.ScopedInstance, currentType, typeConcrete))
            {
                var instance = container.ScopedInstance[currentType][typeConcrete];
                return instance;
            }

            if (bindInformation!.LifeCycle == LifeCycle.Scoped &&
                !LifeCycleUtil.IsScopedInstanced(ref container.ScopedInstance, currentType, typeConcrete))
            {
                var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                var instance = genericMethod.Invoke(null,
                    new object[]
                    {
                        InstanceUtil.ConstructorUtils.GetConstructor(currentType, ConstructorConventionsSequence.First),
                        container
                    });
                container.ScopedInstance[currentType].Add(typeConcrete, instance);
                return instance;
            }


            if (bindInformation!.LifeCycle == LifeCycle.Transient)
            {
                var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[]
                    {
                        InstanceUtil.ConstructorUtils.GetConstructor(currentType, ConstructorConventionsSequence.First),
                        container
                    });
                return obj;
            }

            return null;
        }
    }
}