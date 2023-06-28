using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JSContainer.JSExceptions;
using JSContainer.Utils.Instance;
using JSContainer.Utils;

namespace JSContainer.Binding.BindInfo
{
    public static class BindInfoExtension
    {
        internal static void AddParameterExpressions(this BindInformation bindInformation,
            IReadOnlyCollection<Type> parameterExpressions)
        {
            foreach (var parameter in parameterExpressions)
            {
                bindInformation.Parameters.Add(parameter, parameter);
            }
        }

        internal static void AddContracts(this BindInformation bindInformation, IReadOnlyCollection<Type> contracts)
        {
            bindInformation.ContractsTypes.AddRange(contracts);
        }

        internal static void AddArguments(this BindInformation bindInformation, IReadOnlyCollection<object> arguments)
        {
            if (arguments.Count > bindInformation.Parameters.Count)
                JsExceptions.ConstructorException.IsWrongParamsCountException(bindInformation.CurrentType,
                    bindInformation.Parameters.Count);

            var argumentsTypes = arguments.Select(x => x.GetType()).ToArray();

            foreach (var parameter in bindInformation.Parameters)
            {
                var parameterType = parameter.Key;

                if (InstanceUtil.IsInterfaceAndBinded(parameterType, bindInformation.CurrentContainer) &&
                    bindInformation.CurrentContainer.ContractsInfo[parameterType].Last() != null)
                {
                    parameterType = bindInformation.CurrentContainer.ContractsInfo[parameterType].Last();
                }
                else if (parameter.Key.IsInterface)
                {
                    parameterType = parameter.Key;
                }

                bindInformation.ArgumentsMap.Add(parameterType,
                    argumentsTypes.First(x => parameterType.IsAssignableFrom(x)));
            }
        }
    }
}