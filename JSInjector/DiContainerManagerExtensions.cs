using System;
using System.Collections.Generic;
using JSInjector.Binding.BindInfo;
using JSInjector.JSExceptions;

namespace JSInjector
{
    public static class DiContainerManagerExtensions
    {
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