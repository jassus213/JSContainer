using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace JSInjector
{
    public class ObjCreator
    {
        private readonly DiContainer _diContainer;
        public ObjCreator(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public Object TryCreateObj(Type type, ParameterInfo[] parameterInfos)
        {
            List<object> parameters = new List<object>();
            var objInfo = _diContainer.ContainerInfo[type];

            if (objInfo.Key)
                return objInfo.Value;
            
            try
            {
                if (parameterInfos == null)
                {
                    var value = Activator.CreateInstance(type);
                    RewriteContainerInfo(type, value);
                    return value;
                }


                foreach (var param in parameterInfos)
                {
                    var typeOfParam = param.ParameterType;
                    if (objInfo.Key == false)
                    {
                        var paramInfo = GetParamsInfo(typeOfParam, _diContainer.BindInfoMap[typeOfParam].ContractsTypes.ToArray(), CallingConventions.HasThis);
                        parameters.Add(TryCreateObj(typeOfParam, paramInfo));
                    }
                    else
                    {
                        parameters.Add(objInfo.Value);
                        Console.WriteLine($"{objInfo.Value.GetType()} Already binded");
                    }
                }
                
                var obj = Activator.CreateInstance(type, parameters.ToArray());
                RewriteContainerInfo(type, obj);
                
                return obj;
            }
            catch (Exception e)
            {
                Assert.Fail("Error while building " + type);
            }

            return null;
        }

        public TContract TryCreateObj<TContract>()
        {
            var constructorInfo = typeof(TContract).GetConstructors().First();
            var parameterInfos = constructorInfo.GetParameters();
            var parameters = GetParameters(parameterInfos);
            var body = Expression.New(constructorInfo, parameters);
            var func = Expression.Lambda<Func<ParameterInfo[], TContract>>(body, parameters.ToArray()).Compile();
            var obj = func(parameterInfos);

            return obj;
        }

        private IEnumerable<ParameterExpression> GetParameters(ParameterInfo[] requiredParameters)
        {
            var parametersInfo = new List<ParameterExpression>();
            
            foreach (var parameter in requiredParameters)
            {
                var parameterType = parameter.ParameterType;
                if (!_diContainer.ContainerInfo[parameterType].Key)
                {
                    var param = Expression.Parameter(parameterType);
                    parametersInfo.Add(param);
                }
                else
                {
                    var param = Expression.Parameter(parameterType);
                    parametersInfo.Add(param);
                }
            }

            return parametersInfo.ToArray();
        }

        public Object TryCreateObj(Type type)
        {
            var parameters = GetParamsInfo(type, _diContainer.AllowedTypes.ToArray(), CallingConventions.Any);
            var obj = TryCreateObj(type, parameters);
            return obj;
        }

        private void RewriteContainerInfo(Type type, object obj)
        {
            if (_diContainer.ContainerInfo.Remove(type))
            {
                _diContainer.ContainerInfo.Add(type, new KeyValuePair<bool, object>(true, obj));
            }
        }
        
        public ParameterInfo[] GetParamsInfo(Type type, Type[] contractTypes, CallingConventions callingConventions)
        {
            var parameterInfos = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                callingConventions, contractTypes, null)?.GetParameters();

            return parameterInfos;
        }
    }
}