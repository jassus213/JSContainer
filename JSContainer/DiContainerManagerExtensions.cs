using System;
using System.Collections.Generic;
using JSContainer.Binding.BindInfo;
using JSContainer.Binding.BindInfo.Factory;
using JSContainer.Common.Enums;
using JSContainer.Common.TypeInstancePair;
using JSContainer.JSExceptions;

namespace JSContainer
{
    public static class DiContainerManagerExtensions
    {
        internal static void InitializeBindInfo(this DiContainer currentContainer, Type type, BindInformation bindInformation,
            (bool IsCreated, TypeInstancePair TypeInstancePair) tuple)
        {
            if (!currentContainer.ContainerInfo.ContainsKey(type) && !type.IsInterface)
            {
                currentContainer.ContainerInfo.Add(type, tuple);
                currentContainer.BindInfoMap.Add(type, bindInformation);
                return;
            }

            throw JsExceptions.BindException.AlreadyBindedException(type);
        }

        internal static void InitializeFromResolve(this DiContainer currentContainer, Type type, BindType bindType,
            (bool IsCreated, TypeInstancePair TypeInstancePair) tuple, LifeTime lifeTime)
        {
            var bindInfo = BindInfoFactory.Create(type, bindType, currentContainer, lifeTime);
            RewriteContainerInfo(ref currentContainer.ContainerInfo, type, tuple);
            RewriteBindInfo(ref currentContainer.BindInfoMap, type, bindInfo);
        }

        internal static void ReWriteInstanceInfo(this DiContainer currentContainer, Type type, BindInformation bindInformation,
            (bool IsCreated, TypeInstancePair TypeInstancePair) tuple)
        {
            RewriteContainerInfo(ref currentContainer.ContainerInfo, type, tuple);
            RewriteBindInfo(ref currentContainer.BindInfoMap, type, bindInformation);
        }
        
        internal static BindInformation GetBindInfo(this DiContainer container, Type type, BindType bindType, LifeTime lifeTime)
        {
            if (container.BindInfoMap.ContainsKey(type))
                return container.BindInfoMap[type];

            return BindInfoFactory.Create(type, bindType, container, lifeTime);
        }

        private static void RewriteBindInfo(ref Dictionary<Type, BindInformation> bindInformationMap, Type type, BindInformation bindInformation)
        {
            if (!bindInformationMap.ContainsKey(type))
            {
                throw JsExceptions.BindException.NotBindedException(type);
            }
            
            bindInformationMap[type] = bindInformation;
        }

        private static void RewriteContainerInfo(ref Dictionary<Type, (bool IsCreated, TypeInstancePair TypeInstancePair)> containerInfo, Type type,
            (bool IsCreated, TypeInstancePair TypeInstancePair) tuple)
        {
            if (!containerInfo.ContainsKey(type))
            {
                throw JsExceptions.BindException.NotBindedException(type);
            }

            containerInfo[type] = tuple;
        }
    }
}