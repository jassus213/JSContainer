using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.DiFactories;
using JSInjector.JSExceptions;

namespace JSInjector.Utils
{
    internal static class DiContainerManager
    {
        internal static object SearchInstance<TConcrete>(DiContainer container)
        {
            var type = typeof(TConcrete);

            if (container.ContainerInfo[type].Key)
                return container.ContainerInfo[type].Value;

            if (InstanceUtil.ParametersUtil.HasCircularDependency(type,
                    InstanceUtil.ParametersUtil.GetParametersExpression(type)))
                return null;
            
            var baseMethods = typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var currentMethod = baseMethods.First(x =>
                x.GetGenericArguments().Length - 1 == container.BindInfoMap[type].ParameterExpressions.Count &&
                x.Name == "CreateInstance");
            var genericMethod =
                currentMethod.MakeGenericMethod(GenericArgumentsMap(type,
                    container.BindInfoMap[type].ParameterExpressions));
            var obj = genericMethod.Invoke(null,
                new object[] { InstanceUtil.ConstructorUtils.GetConstructor(type), container });
            return obj;
        }

        private static Type[] GenericArgumentsMap(Type type, IEnumerable<ParameterExpression> arguments)
        {
            var result = new List<Type>();

            foreach (var argument in arguments)
            {
                result.Add(argument.Type);
            }

            result.Add(type);
            return result.ToArray();
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
            KeyValuePair<bool, object> keyValuePair)
        {
            currentContainer.BindQueue.Dequeue();
            var bindInfo = new BindInfo(type, bindTypes, InstanceType.Default);
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
            InstanceType instanceType)
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