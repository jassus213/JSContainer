using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.DiFactories;
using JSInjector.JSExceptions;
using JSInjector.Utils;

namespace JSInjector
{
    internal static class DiContainerManager
    {
        internal static object SearchInstance<TInstance, TConcrete>(DiContainer container)
        {
            var type = typeof(TInstance);
            var currentType = type;
            BindInfo bindInfo = null;

            if (IsInterface(currentType, container))
            {
                currentType = container.ContractsInfo[type].Last();

                bindInfo = container.BindInfoMap[currentType];
                
                if (!bindInfo.ContractsTypes.Contains(type))
                    JsExceptions.BindException.ContractNotBindedToInstance(type, currentType);
            }
            else
            {
                bindInfo = container.BindInfoMap[currentType];
            }

            if (InstanceUtil.ParametersUtil.HasCircularDependency(type,
                    InstanceUtil.ParametersUtil.GetParametersExpression(currentType)))
                return null;

            if (bindInfo!.LifeCycle == LifeCycle.Singleton &&
                LifeCycleUtil.IsSingletonInstanced(container, currentType))
            {
                return container.ContainerInfo[currentType].Value;
            }

            if (bindInfo!.LifeCycle == LifeCycle.Singleton && !LifeCycleUtil.IsSingletonInstanced(container, currentType))
            {
                var genericMethod = FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[] { InstanceUtil.ConstructorUtils.GetConstructor(currentType), container });
                return obj;
            }


            if (bindInfo!.LifeCycle == LifeCycle.Scoped &&
                LifeCycleUtil.IsScopedInstanced(container, currentType, typeof(TConcrete)))
            {
                var instance = container.ScopedInstance[currentType][typeof(TConcrete)];
                return instance;
            }

            if (bindInfo!.LifeCycle == LifeCycle.Scoped &&
                !LifeCycleUtil.IsScopedInstanced(container, currentType, typeof(TConcrete)))
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

        private static bool IsInterface(Type type, DiContainer container)
        {
            var currentType = type;

            if (type.IsInterface && container.ContractsInfo.ContainsKey(type))
            {
                currentType = container.ContractsInfo[type].Last();

                if (!container.BindInfoMap.ContainsKey(currentType))
                {
                    JsExceptions.BindException.NotBindedException(currentType);
                }
                
                return true;
            }

            return false;
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