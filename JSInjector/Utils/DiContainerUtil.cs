using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JSInjector.Binding.BindInfo;
using JSInjector.JSExceptions;

namespace JSInjector.Utils
{
    internal static class DiContainerUtil
    {
        internal static IReadOnlyCollection<object> SearchInstances(DiContainer currentContainer, IEnumerable<ParameterExpression> parametersExpressions)
        {
            var result = new List<object>();
            
            foreach (var param in parametersExpressions)
            {
                var paramType = param.Type;
                if (currentContainer.ContainerInfo.ContainsKey(paramType) && currentContainer.ContainerInfo[paramType].Key)
                {
                    result.Add(currentContainer.ContainerInfo[paramType].Value);
                }
                else if (currentContainer.ContainerInfo.ContainsKey(paramType) && !currentContainer.ContainerInfo[paramType].Key)
                {
                    var parameterInstance = ObjectInitializer.CreateInstanceByType(currentContainer, paramType);
                    currentContainer.ReWriteContainerInfo(paramType, new KeyValuePair<bool, object>(true, parameterInstance));
                    result.Add(parameterInstance);
                }
            }

            return result.ToArray();
        }
        
        internal static void InitializeBindInfo(this DiContainer currentContainer, Type type, BindInfo bindInfo, KeyValuePair<bool, object> keyValuePair)
        {
            if (!currentContainer.ContainerInfo.ContainsKey(type))
            {
                currentContainer.BindQueue.Enqueue(new KeyValuePair<Type, KeyValuePair<bool, object>>(type, keyValuePair));
                currentContainer.ContainerInfo.Add(type, keyValuePair);
                currentContainer.BindInfoMap.Add(type, bindInfo);
            }
        }
        
        internal static void InitializeFromResolve(this DiContainer currentContainer, Type type, BindTypes bindTypes, KeyValuePair<bool, object> keyValuePair)
        {
            currentContainer.BindQueue.Dequeue();
            var bindInfo = new BindInfo(type, bindTypes, InstanceType.Default);
            ReWriteContainerInfo(currentContainer, type, keyValuePair);
            ReWriteBindInfo(currentContainer, type, bindInfo);
        }
        
        internal static void InitializeFactoryInfoMap(this DiContainer container, Type type, FactoryBindInfo factoryBindInfo)
        {
            container.FactoryBindInfoMap.Add(type, factoryBindInfo);
        }
        
        internal static BindInfo GetBindInfo(this DiContainer container, Type type, BindTypes bindType, InstanceType instanceType)
        {
            if (container.BindInfoMap.ContainsKey(type))
                return container.BindInfoMap[type];

            return new BindInfo(type, bindType, instanceType);
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

        internal static void ReWriteContainerInfo(this DiContainer currentContainer, Type type, KeyValuePair<bool, object> keyValuePair)
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