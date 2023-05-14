using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSContainer.Binding.BindInfo;
using JSContainer.DiFactories;
using JSContainer.Utils.Instance;

namespace JSContainer.Services
{
    public static class InstanceFactoryService
    {
        internal static object FindAndInvokeMethod(DiContainer diContainer, Type instanceType,
            BindInformation bindInformation)
        {
            var baseMethod = typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).First(x =>
                x.GetGenericArguments().Length - 1 == bindInformation.ParameterExpressions.Count);
            var genericMethod = baseMethod.MakeGenericMethod(InstanceUtil.GenericParameters
                .GenericArgumentsMap(instanceType, bindInformation.ParameterExpressions.Values)
                .ToArray());
            var instance = genericMethod.Invoke(diContainer,
                new object[]
                {
                    InstanceUtil.ConstructorUtils.GetConstructor(instanceType, ConstructorConventionsSequence.First),
                    diContainer
                });
            return instance;
        }

        internal static MethodInfo FindMethod(DiContainer container, Type currentType)
        {
            var baseMethods = typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var currentMethod = baseMethods.First(x =>
                x.GetGenericArguments().Length - 1 == container.BindInfoMap[currentType].ParameterExpressions.Count &&
                x.Name == "CreateInstance");
            var genericMethod =
                currentMethod.MakeGenericMethod(InstanceUtil.GenericParameters.GenericArgumentsMap(currentType,
                    container.BindInfoMap[currentType].ParameterExpressions.Values).ToArray());
            return genericMethod;
        }
    }
}