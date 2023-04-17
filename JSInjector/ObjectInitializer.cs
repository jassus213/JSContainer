using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.Utils;

namespace JSInjector
{
    internal class ObjectInitializer
    {
        private readonly DiContainer _diContainer;
        public ObjectInitializer(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public TContract CreateInstance<TContract>()
        {
            var constructorInfo = InstanceUtil.ConstructorUtils.GetConstructor(typeof(TContract));
            var parameterInfos = constructorInfo.GetParameters();
            var parameters =
                InstanceUtil.ParametersUtil.GetParametersExpression(parameterInfos.Select(x => x.ParameterType).ToArray());

            var instances = SearchInstances(parameters);

            var obj = CreateInstance(typeof(TContract), parameters, instances);
            _diContainer.ReWriteContainerInfo(typeof(TContract), new KeyValuePair<bool, object>(true, obj));
            return (TContract)obj;
        }

        private static object CreateInstance(Type instanceType, IEnumerable<ParameterExpression> parameterExpressions, IReadOnlyCollection<object> parametersInstances)
        {
            var parameters = parameterExpressions.Select(x => x.Type);
            
            var requiredConstructor = InstanceUtil.ConstructorUtils.GetConstructor(instanceType, parameters.Count());
            
            var obj = requiredConstructor.Invoke(parametersInstances.ToArray());
            return obj;
        }
        
        private object CreateInstance(Type type)
        {
            IEnumerable<ParameterExpression> parameters = null;
            if (_diContainer.BindInfoMap[type].BindType == BindTypes.SelfTo || _diContainer.BindInfoMap[type].BindType == BindTypes.InterfacesAndSelfTo)
            {
                parameters = _diContainer.BindInfoMap[type].ParameterExpressions;
            }
            else
            {
                parameters = InstanceUtil.ParametersUtil.GetParametersExpression(new[] { type });
            }
            
            
            var requiredMethod =
                this.GetType().GetMethod("CreateInstance", BindingFlags.NonPublic | BindingFlags.Static);
            var requiredParams = new List<object>();
            requiredParams.Add(type);
            requiredParams.Add(parameters);
            requiredParams.Add(SearchInstances(parameters));
            
            var obj = requiredMethod.Invoke(this, requiredParams.ToArray());
            return obj;
        }

        private IReadOnlyCollection<object> SearchInstances(IEnumerable<ParameterExpression> parametersExpressions)
        {
            var result = new List<object>();
            
            foreach (var param in parametersExpressions)
            {
                var paramType = param.Type;
                if (_diContainer.ContainerInfo.ContainsKey(paramType) && _diContainer.ContainerInfo[paramType].Key)
                {
                    result.Add(_diContainer.ContainerInfo[paramType].Value);
                }
                else if (_diContainer.ContainerInfo.ContainsKey(paramType) && !_diContainer.ContainerInfo[paramType].Key)
                {
                    var paramInstance = CreateInstance(paramType);
                    _diContainer.ReWriteContainerInfo(paramType, new KeyValuePair<bool, object>(true, paramInstance));
                    result.Add(paramInstance);
                }
            }

            return result.ToArray();
        }
    }
}