using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.DiFactories;
using JSInjector.JSExceptions;
using JSInjector.Utils;

namespace JSInjector
{
    internal static class DiContainerManager
    {
        internal static object SearchInstance<TInstance, TConcrete>(DiContainer container) where TConcrete : class where TInstance : class
        {
            var type = typeof(TInstance);
            var currentType = type;
            BindInfo bindInfo = null;


            if (InstanceUtil.IsInterfaceAndBinded(currentType, container))
            {
                currentType = container.ContractsInfo[type].Last();

                bindInfo = container.BindInfoMap[currentType];

                if (!bindInfo.ContractsTypes.Contains(type))
                    JsExceptions.BindException.ContractNotBindedToInstance(type, currentType);
            }
            else if (container.BindInfoMap.ContainsKey(currentType))
            {
                bindInfo = container.BindInfoMap[currentType];
            }
            
            
            if (container.BindInfoMap[typeof(TConcrete)].ArgumentsMap.ContainsKey(currentType)) // Get argument from TConcrete
            {
                bindInfo = container.BindInfoMap[typeof(TConcrete)];
                var instance = bindInfo.ArgumentsMap[currentType];
                return instance;
            }
            
            if (InstanceUtil.ParametersUtil.HasCircularDependency(type,
                    InstanceUtil.ParametersUtil.GetParametersExpression(currentType)))
                return null;

            if (bindInfo!.LifeCycle == LifeCycle.Singleton &&
                container.IsSingletonInstanced(currentType))
            {
                return container.ContainerInfo[currentType].Value;
            }

            if (bindInfo!.LifeCycle == LifeCycle.Singleton &&
                !container.IsSingletonInstanced(currentType))
            {
                var genericMethod = FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[] { InstanceUtil.ConstructorUtils.GetConstructor(currentType), container });
                return obj;
            }


            if (bindInfo!.LifeCycle == LifeCycle.Scoped &&
                container.IsScopedInstanced(currentType, typeof(TConcrete)))
            {
                var instance = container.ScopedInstance[currentType][typeof(TConcrete)];
                return instance;
            }

            if (bindInfo!.LifeCycle == LifeCycle.Scoped &&
                !container.IsScopedInstanced(currentType, typeof(TConcrete)))
            {
                var genericMethod = FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[] { InstanceUtil.ConstructorUtils.GetConstructor(currentType), container });
                container.ScopedInstance[currentType].Add(typeof(TConcrete), obj);
                return obj;
            }


            if (bindInfo!.LifeCycle == LifeCycle.Transient)
            {
                var genericMethod = FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[] { InstanceUtil.ConstructorUtils.GetConstructor(currentType), container });
                return obj;
            }

            return null;
        }

        private static MethodInfo FindMethod(DiContainer container, Type currentType)
        {
            var baseMethods = typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var currentMethod = baseMethods.First(x =>
                x.GetGenericArguments().Length - 1 == container.BindInfoMap[currentType].ParameterExpressions.Count &&
                x.Name == "CreateInstance");
            var genericMethod =
                currentMethod.MakeGenericMethod(InstanceUtil.GenericParameters.GenericArgumentsMap(currentType,
                    container.BindInfoMap[currentType].ParameterExpressions).ToArray());
            return genericMethod;
        }
    }
}