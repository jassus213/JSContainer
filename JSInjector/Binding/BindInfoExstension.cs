using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JSInjector.Binding
{
    public static class BindInfoExtension
    {
        internal static void SetParameterExpressions(this BindInfo.BindInfo bindInfo, IReadOnlyCollection<ParameterExpression> parameterExpressions)
        {
            bindInfo.ParameterExpressions.AddRange(parameterExpressions);
        }

        internal static void AddContracts(this BindInfo.BindInfo bindInfo, IReadOnlyCollection<Type> contracts)
        {
            bindInfo.ContractsTypes.AddRange(contracts);
        }
    }
}