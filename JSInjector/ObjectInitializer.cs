using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.Utils;

namespace JSInjector
{
    internal static class ObjectInitializer
    {
        /// <summary>
        /// This methods Invokes in Initialize() in DiContainer
        /// </summary>
        
        public static TContract CreateInstance<TContract>(DiContainer diContainer)
        {
            var constructorInfo = InstanceUtil.ConstructorUtils.GetConstructor(typeof(TContract));
            var parameterInfos = InstanceUtil.ParametersUtil.GetParametersInfo(constructorInfo);
            var parameters =
                InstanceUtil.ParametersUtil.Map(parameterInfos.Select(x => x.ParameterType).ToArray());
            object result = null;

            if (!InstanceUtil.ParametersUtil.HasCircularDependency(typeof(TContract), parameters))
            {
                var instances = DiContainerUtil.SearchInstances(diContainer, parameters);

                result = CreateInstance(typeof(TContract), parameters, instances);
                diContainer.ReWriteContainerInfo(typeof(TContract), new KeyValuePair<bool, object>(true, result));
            }

            return (TContract)result;
        }
        
        internal static object CreateInstanceByType(DiContainer diContainer, Type type)
        {
            IEnumerable<ParameterExpression> parameters = null;
            if (diContainer.BindInfoMap[type].BindType == BindTypes.SelfTo ||
                diContainer.BindInfoMap[type].BindType == BindTypes.InterfacesAndSelfTo)
            {
                parameters = diContainer.BindInfoMap[type].ParameterExpressions;
            }
            else
            {
                parameters = InstanceUtil.ParametersUtil.GetParametersExpression(type);
            }

            var requiredMethod =
                typeof(ObjectInitializer).GetMethod("CreateInstance", BindingFlags.NonPublic | BindingFlags.Static);

            var requiredParams = new List<object>();
            requiredParams.Add(type);
            requiredParams.Add(parameters);
            requiredParams.Add(DiContainerUtil.SearchInstances(diContainer, parameters));

            var obj = requiredMethod.Invoke(typeof(ObjectInitializer), requiredParams.ToArray());
            return obj;
        }

        private static object CreateInstance(Type instanceType, IEnumerable<ParameterExpression> parameterExpressions,
            IReadOnlyCollection<object> parametersInstances)
        {
            var parameters = parameterExpressions.Select(x => x.Type);

            var requiredConstructor = InstanceUtil.ConstructorUtils.GetConstructor(instanceType, parameters);

            var obj = requiredConstructor.Invoke(parametersInstances.ToArray());
            return obj;
        }
    }
}
