using System;
using System.Linq;
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
                x.GetGenericArguments().Length - 1 == bindInformation.Parameters.Count);
            var genericMethod = baseMethod.MakeGenericMethod(InstanceUtil.GenericParameters
                .GenericArgumentsMap(instanceType, bindInformation.Parameters.Values)
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
                x.GetGenericArguments().Length - 1 == container.BindInfoMap[currentType].Parameters.Count &&
                x.Name == "CreateInstance");
            var genericMethod =
                currentMethod.MakeGenericMethod(InstanceUtil.GenericParameters.GenericArgumentsMap(currentType,
                    container.BindInfoMap[currentType].Parameters.Values).ToArray());
            return genericMethod;
        }
    }
}