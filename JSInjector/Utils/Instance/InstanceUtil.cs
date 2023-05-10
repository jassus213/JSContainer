using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JSInjector.Binding.BindInfo;
using JSInjector.JSExceptions;
using JSInjector.Utils.Instance;

namespace JSInjector.Utils
{
    internal static class InstanceUtil
    {
        internal static bool IsInterfaceAndBinded(Type type, DiContainer container)
        {
            if (type.IsInterface && container.ContractsInfo.ContainsKey(type))
            {
                var currentType = container.ContractsInfo[type].Last();

                if (!container.BindInfoMap.ContainsKey(currentType))
                {
                    JsExceptions.BindException.NotBindedException(currentType);
                }

                return true;
            }

            return false;
        }

        internal static bool IsInterfaceAndBinded(Type type, ref Dictionary<Type, IEnumerable<Type>> contractsInfo,
            ref Dictionary<Type, BindInformation> bindInformationMap)
        {
            if (type.IsInterface && contractsInfo.ContainsKey(type))
            {
                var currentType = contractsInfo[type].Last();

                if (!bindInformationMap.ContainsKey(currentType))
                {
                    JsExceptions.BindException.NotBindedException(currentType);
                }

                return true;
            }

            return false;
        }

        internal static class ParametersUtil
        {
            internal static bool HasCircularDependency(Dictionary<Type, IEnumerable<Type>> contractsInfo, Type type,
                IEnumerable<ParameterExpression> parameterExpressions)
            {
                foreach (var param in parameterExpressions)
                {
                    var paramType = param.Type;
                    if (param.Type.IsInterface && contractsInfo.ContainsKey(param.Type))
                    {
                        paramType = contractsInfo[param.Type].Last();
                    }

                    var parametersOfParam = GetParametersExpression(paramType);
                    var map = Map(new[] { type }).First();
                    if (parametersOfParam.Where(x => x.Type == map.Type).ToArray().Length != 0)
                    {
                        throw JsExceptions.BindException.CircularDependency(type, paramType);
                    }
                }

                return false;
            }

            internal static IReadOnlyCollection<ParameterExpression> Map(Type[] requiredParameters)
            {
                var result = new List<ParameterExpression>();

                for (int i = 0; i < requiredParameters.Length; i++)
                {
                    var name = "Parameter " + i;
                    var parameter = Expression.Parameter(requiredParameters[i], name);
                    result.Add(parameter);
                }

                return result;
            }

            internal static IReadOnlyCollection<ParameterInfo> GetParametersInfo(Type type, Type[] contractTypes,
                CallingConventions callingConventions = default)
            {
                var parameterInfos = type.GetConstructor(BindingFlags.Default, null,
                    callingConventions, contractTypes, null)?.GetParameters();

                return parameterInfos;
            }

            internal static IReadOnlyCollection<ParameterInfo> GetParametersInfo(Type constructorType)
            {
                return GetParametersInfo(ConstructorUtils.GetConstructor(constructorType,
                    ConstructorConventionsSequence.First));
            }

            private static IReadOnlyCollection<ParameterInfo> GetParametersInfo(ConstructorInfo constructorInfo)
            {
                return constructorInfo.GetParameters();
            }

            internal static IReadOnlyCollection<ParameterExpression> GetParametersExpression(Type constructorType)
            {
                var parametersInfo =
                    GetParametersInfo(ConstructorUtils.GetConstructor(constructorType,
                        ConstructorConventionsSequence.First));
                var parameters = parametersInfo.Select(x => x.ParameterType);
                return GetParametersExpression(parameters.ToArray());
            }

            internal static IReadOnlyCollection<ParameterExpression> GetParametersExpression(
                Type[] requiredParameters)
            {
                var result = new List<ParameterExpression>();

                for (int i = 0; i < requiredParameters.Length; i++)
                {
                    var name = "Parameter " + i;
                    var parameter = Expression.Parameter(requiredParameters[i], name);
                    result.Add(parameter);
                }

                return result;
            }
        }

        internal static class GenericParameters
        {
            internal static IReadOnlyCollection<Type> GenericArgumentsMap(Type type,
                IEnumerable<ParameterExpression> arguments)
            {
                var result = arguments.Select(x => x.Type).ToList();

                result.Add(type);
                return result.ToArray();
            }
        }

        internal static class ContractsUtil
        {
            internal static IReadOnlyCollection<Type> GetContractsExpression(Type type)
            {
                var contracts = type.GetInterfaces();
                return contracts;
            }
        }

        internal static class ConstructorUtils
        {
            private static IReadOnlyCollection<ConstructorInfo> GetConstructors(Type type)
            {
                return type.GetConstructors();
            }

            internal static ConstructorInfo GetConstructor(Type type, int requiredParamsCount)
            {
                var constructors = type.GetConstructors().Where(x => x.GetParameters().Length == requiredParamsCount)
                    .ToArray();
                if (constructors.Length > 1)
                    JsWarnings.ConstructorWarnings.LotConstructorReturnedWarning(constructors.Count(),
                        constructors.ToArray());
                if (constructors.Length == 0)
                    JsExceptions.ConstructorException.ConstructorIsNullException(type);

                return constructors.First();
            }

            internal static ConstructorInfo GetConstructor(Type type, Type[] requiredParams)
            {
                var constructorInfo = type.GetConstructor(requiredParams);
                if (constructorInfo == null)
                    JsExceptions.ConstructorException.ConstructorIsNullException(type);

                return constructorInfo;
            }

            internal static ConstructorInfo GetConstructor(Type type,
                ConstructorConventionsSequence constructorConventionSequence)
            {
                return constructorConventionSequence switch
                {
                    ConstructorConventionsSequence.First => GetConstructors(type).First(),
                    ConstructorConventionsSequence.Last => GetConstructors(type).Last(),
                    _ => null
                };
            }
        }
    }
}