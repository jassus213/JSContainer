using System;
using System.Linq;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.DiFactories;
using JSInjector.Utils;
using JSInjector.Utils.Instance;

namespace JSInjector.Service
{
    public static class InstanceFactoryService
    {
        internal static object FindAndInvokeMethod(DiContainer diContainer, Type instanceType, BindInformation bindInformation)
        {
            var baseMethods =
                typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            var currentMethod = baseMethods.First(x =>
                x.GetGenericArguments().Length - 1 ==
                bindInformation.ParameterExpressions.Count && x.Name == "CreateInstance");
            var genericMethod = currentMethod.MakeGenericMethod(InstanceUtil.GenericParameters
                .GenericArgumentsMap(instanceType, bindInformation.ParameterExpressions)
                .ToArray());
            var instance = genericMethod.Invoke(diContainer,
                new object[] { InstanceUtil.ConstructorUtils.GetConstructor(instanceType, ConstructorConventionsSequence.First), diContainer});
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
                    container.BindInfoMap[currentType].ParameterExpressions).ToArray());
            return genericMethod;
        }
        
    }
}