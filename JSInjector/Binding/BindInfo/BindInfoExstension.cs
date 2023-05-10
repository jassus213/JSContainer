using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JSInjector.JSExceptions;
using JSInjector.Utils;

namespace JSInjector.Binding.BindInfo
{
    public static class BindInfoExtension
    {
        internal static void AddParameterExpressions(this BindInformation bindInformation, IReadOnlyCollection<ParameterExpression> parameterExpressions)
        {
            bindInformation.ParameterExpressions.AddRange(parameterExpressions);
        }

        internal static void AddContracts(this BindInformation bindInformation, IReadOnlyCollection<Type> contracts)
        {
            bindInformation.ContractsTypes.AddRange(contracts);
        }
        
        internal static void AddArguments(this BindInformation bindInformation, IReadOnlyCollection<object> arguments)
        {
            if (arguments.Count > bindInformation.ParameterExpressions.Count)
                JsExceptions.ConstructorException.IsWrongParamsCountException(bindInformation.CurrentType, bindInformation.ParameterExpressions.Count);
            
            var argumentsTypes = arguments.Select(x => x.GetType()).ToArray();

            foreach (var parameter in bindInformation.ParameterExpressions)
            {
                var parameterType = parameter.Type;

                if (InstanceUtil.IsInterfaceAndBinded(parameter.Type, bindInformation.CurrentContainer) && 
                    bindInformation.CurrentContainer.ContractsInfo[parameterType].Last() != null)
                    parameterType = bindInformation.CurrentContainer.ContractsInfo[parameterType].Last();
                else if (parameter.Type.IsInterface)
                {
                    parameterType = parameter.Type;
                }
                
                bindInformation.ArgumentsMap.Add(parameterType, argumentsTypes.First(x => parameterType.IsAssignableFrom(x)));
            }
        }
    }
}