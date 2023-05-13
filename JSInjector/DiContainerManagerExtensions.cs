using System;
using System.Collections.Generic;
using JSInjector.Binding.BindInfo;
using JSInjector.Binding.BindInfo.Factory;
using JSInjector.Common.Enums;
using JSInjector.Common.TypeInstancePair;
using JSInjector.JSExceptions;

namespace JSInjector
{
    public static class DiContainerManagerExtensions
    {
        internal static void InitializeBindInfo(this DiContainer currentContainer, Type type, BindInformation bindInformation,
            KeyValuePair<bool, TypeInstancePair> keyValuePair)
        {
            if (!currentContainer.ContainerInfo.ContainsKey(type) && !type.IsInterface)
            {
                currentContainer.ContainerInfo.Add(type, keyValuePair);
                currentContainer.BindInfoMap.Add(type, bindInformation);
                return;
            }

            throw JsExceptions.BindException.AlreadyBindedException(type);
        }

        internal static void InitializeFromResolve(this DiContainer currentContainer, Type type, BindType bindType,
            KeyValuePair<bool, TypeInstancePair> keyValuePair, LifeCycle lifeCycle)
        {
            var bindInfo = BindInfoFactory.Create(type, bindType, InstanceType.Default, currentContainer, lifeCycle);
            RewriteContainerInfo(ref currentContainer.ContainerInfo, type, keyValuePair);
            RewriteBindInfo(ref currentContainer.BindInfoMap, type, bindInfo);
        }

        internal static void ReWriteInstanceInfo(this DiContainer currentContainer, Type type, BindInformation bindInformation,
            KeyValuePair<bool, TypeInstancePair> keyValuePair)
        {
            RewriteContainerInfo(ref currentContainer.ContainerInfo, type, keyValuePair);
            RewriteBindInfo(ref currentContainer.BindInfoMap, type, bindInformation);
        }

        internal static void InitializeFactoryInfoMap(this DiContainer container, Type type,
            FactoryBindInfo factoryBindInfo)
        {
            container.FactoryBindInfoMap.Add(type, factoryBindInfo);
        }

        internal static BindInformation GetBindInfo(this DiContainer container, Type type, BindType bindType,
            InstanceType instanceType, LifeCycle lifeCycle)
        {
            if (container.BindInfoMap.ContainsKey(type))
                return container.BindInfoMap[type];

            return BindInfoFactory.Create(type, bindType, instanceType, container, lifeCycle);
        }

        private static void RewriteBindInfo(ref Dictionary<Type, BindInformation> bindInformationMap, Type type, BindInformation bindInformation)
        {
            if (!bindInformationMap.ContainsKey(type))
            {
                throw JsExceptions.BindException.NotBindedException(type);
            }
            
            bindInformationMap[type] = bindInformation;
        }

        private static void RewriteContainerInfo(ref Dictionary<Type, KeyValuePair<bool, TypeInstancePair>> containerInfo, Type type,
            KeyValuePair<bool, TypeInstancePair> keyValuePair)
        {
            if (!containerInfo.ContainsKey(type))
            {
                throw JsExceptions.BindException.NotBindedException(type);
            }

            containerInfo[type] = keyValuePair;
        }
    }
}