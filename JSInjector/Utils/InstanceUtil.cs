using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.JSExceptions;

namespace JSInjector.Utils
{
    internal static class InstanceUtil
    {
        internal static class ParametersUtil
        {
            internal static IEnumerable<ParameterExpression> GetParametersExpression(Type[] requiredParameters)
            {
                var result = new List<ParameterExpression>();
            
                for (int i = 0; i < requiredParameters.Length; i++)
                {
                    var name = "Parameter " + i;
                    var parameter = Expression.Parameter(requiredParameters[i],  name);
                    result.Add(parameter);
                }

                return result;
            }
            
            internal static ParameterInfo[] GetParamsInfo(Type type, Type[] contractTypes, CallingConventions callingConventions)
            {
                var parameterInfos = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                    callingConventions, contractTypes, null)?.GetParameters();

                return parameterInfos;
            }
            
            internal static IEnumerable<ParameterExpression> GetParametersExpression(IEnumerable<Type> requiredParameters)
            {
                var result = new List<ParameterExpression>();
                var parametersArray = requiredParameters.ToArray();
            
                for (int i = 0; i < parametersArray.Length; i++)
                {
                    var name = "Parameter " + i;
                    var parameter = Expression.Parameter(parametersArray[i],  name);
                    result.Add(parameter);
                }

                return result;
            }
        }

        internal static class ContractsUtil
        {
            internal static IEnumerable<Type> GetContractsExpression(Type type)
            {
                var contracts = type.GetInterfaces();
                return contracts;
            }
        }

        internal static class ConstructorUtils
        {
            internal static ConstructorInfo GetConstructor(Type type)
            {
                return type.GetConstructors().First();
            }

            internal static ConstructorInfo GetConstructor(Type type, int requiredParams)
            {
                var constructors = type.GetConstructors().Where(x => x.GetParameters().Length == requiredParams).ToArray();
                if (constructors.Length > 1)
                    JsWarnings.ConstructorWarnings.LotConstructorReturnedWarning(constructors.Count(), constructors.ToArray());
                if (constructors.Length == 0)
                    JsExceptions.ConstructorException.ConstructorIsNullException(type);
                
                return constructors.First();
            }
        }

    }
}