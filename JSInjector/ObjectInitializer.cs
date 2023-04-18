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

        private static Func<TArg1, TConcrete> CreateFunc<TArg1, TConcrete>(ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression.Lambda<Func<TArg1, TConcrete>>(Expression.New(constructorInfo, parameterExpressions), parameterExpressions).Compile();
        }
        
        private static Func<TArg1, TArg2, TConcrete> CreateFunc<TArg1, TArg2, TConcrete>(ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression.Lambda<Func<TArg1, TArg2, TConcrete>>(Expression.New(constructorInfo, parameterExpressions), parameterExpressions).Compile();
        }

        internal static Func<TConcrete> CreateInstance<TConcrete>(ConstructorInfo constructorInfo, DiContainer diContainer)
        {
            return null;
        }

        private static TConcrete CreateInstance<TArg1, TConcrete>(ConstructorInfo constructorInfo, DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = CreateFunc<TArg1, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer));
            return obj;
        }
        
        private static TConcrete CreateInstance<TArg1, TArg2, TConcrete>(ConstructorInfo constructorInfo, DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = CreateFunc<TArg1, TArg2, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer), (TArg2)DiContainerUtil.SearchInstance<TArg2>(diContainer));
            return obj;
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
