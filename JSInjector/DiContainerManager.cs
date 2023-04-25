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
        internal static object SearchInstance<TConcrete>(DiContainer container)
        {
            var type = typeof(TConcrete);
            var currentType = type;
            BindInfo bindInfo = null;

            if (type.IsInterface && container.ContractsInfo.ContainsKey(type))
            {
                currentType = container.ContractsInfo[type].Last();

                if (!container.BindInfoMap.ContainsKey(currentType))
                {
                    JsExceptions.BindException.NotBindedException(currentType);
                }
                
                bindInfo = container.BindInfoMap[currentType];
                
                if (!bindInfo.ContractsTypes.Contains(type))
                    JsExceptions.BindException.ContractNotBindedToInstance(currentType, type);
            }
            
            if (type.IsClass && !type.IsAbstract)
                bindInfo = container.BindInfoMap[currentType];

            if (container.ContainerInfo[currentType].Key && bindInfo!.LifeCycle == LifeCycle.Singleton)
                return container.ContainerInfo[currentType].Value;

            if (InstanceUtil.ParametersUtil.HasCircularDependency(type,
                    InstanceUtil.ParametersUtil.GetParametersExpression(currentType)))
                return null;

            if (bindInfo!.LifeCycle == LifeCycle.Transient || bindInfo.LifeCycle == LifeCycle.Scoped)
            {
                var genericMethod = FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[] { InstanceUtil.ConstructorUtils.GetConstructor(currentType), container });
                return obj;
            }
            
            if (bindInfo!.LifeCycle == LifeCycle.Singleton && !container.ContainerInfo[currentType].Key)
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

        internal static void InitializeBindInfo(this DiContainer currentContainer, Type type, BindInfo bindInfo,
            KeyValuePair<bool, object> keyValuePair)
        {
            if (!currentContainer.ContainerInfo.ContainsKey(type))
            {
                currentContainer.BindQueue.Enqueue(
                    new KeyValuePair<Type, KeyValuePair<bool, object>>(type, keyValuePair));
                currentContainer.ContainerInfo.Add(type, keyValuePair);
                currentContainer.BindInfoMap.Add(type, bindInfo);
            }
        }

        internal static void InitializeFromResolve(this DiContainer currentContainer, Type type, BindTypes bindTypes,
            KeyValuePair<bool, object> keyValuePair, LifeCycle lifeCycle)
        {
            currentContainer.BindQueue.Dequeue();
            var bindInfo = new BindInfo(type, bindTypes, InstanceType.Default, currentContainer, lifeCycle);
            ReWriteContainerInfo(currentContainer, type, keyValuePair);
            ReWriteBindInfo(currentContainer, type, bindInfo);
        }

        internal static void ReWriteInstanceInfo(this DiContainer currentContainer, Type type, BindInfo bindInfo,
            KeyValuePair<bool, object> keyValuePair)
        {
            ReWriteContainerInfo(currentContainer, type, keyValuePair);
            ReWriteBindInfo(currentContainer, type, bindInfo);
        }

        internal static void InitializeFactoryInfoMap(this DiContainer container, Type type,
            FactoryBindInfo factoryBindInfo)
        {
            container.FactoryBindInfoMap.Add(type, factoryBindInfo);
        }

        internal static BindInfo GetBindInfo(this DiContainer container, Type type, BindTypes bindType,
            InstanceType instanceType, LifeCycle lifeCycle)
        {
            if (container.BindInfoMap.ContainsKey(type))
                return container.BindInfoMap[type];

            return new BindInfo(type, bindType, instanceType, container, lifeCycle);
        }

        private static void ReWriteBindInfo(this DiContainer currentContainer, Type type, BindInfo bindInfo)
        {
            if (!currentContainer.BindInfoMap.ContainsKey(type))
            {
                JsExceptions.BindException.NotBindedException(type);
                return;
            }

            currentContainer.BindInfoMap.Remove(type);
            currentContainer.BindInfoMap.Add(type, bindInfo);
        }

        private static void ReWriteContainerInfo(this DiContainer currentContainer, Type type,
            KeyValuePair<bool, object> keyValuePair)
        {
            if (!currentContainer.ContainerInfo.ContainsKey(type))
            {
                JsExceptions.BindException.NotBindedException(type);
                return;
            }

            currentContainer.ContainerInfo.Remove(type);
            currentContainer.ContainerInfo.Add(type, keyValuePair);
        }
    }
}