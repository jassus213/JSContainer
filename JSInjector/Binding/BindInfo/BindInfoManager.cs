using System;
using System.Collections.Generic;
using System.Linq;
using JSInjector.Utils;
using JSInjector.Utils.Instance;

namespace JSInjector.Binding.BindInfo
{
    internal static class BindInfoManager
    {
        private static void BindSelf(this BindInformation bindInformation)
        {
            var type = bindInformation.CurrentType;
            var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type, ConstructorConventionsSequence.First);
            var parameters =
                InstanceUtil.ParametersUtil.GetParametersExpression(constructor.GetParameters()
                    .Select(x => x.ParameterType).ToArray());
            bindInformation.AddParameterExpressions(parameters);
        }

        internal static void BindSelfTo(this BindInformation bindInformation)
        {
            BindSelf(bindInformation);
        }

        internal static void BindInterfacesTo(this BindInformation bindInformation, ref Dictionary<Type, IEnumerable<Type>> contractsType)
        {
            var contracts = InstanceUtil.ContractsUtil.GetContractsExpression(bindInformation.CurrentType);
            foreach (var contract in contracts)
            {
                contractsType.Add(contract, new[] { bindInformation.CurrentType });
            }

            bindInformation.AddContracts(contracts);
        }

        internal static void BindInterfacesAndSelfTo(this BindInformation bindInformation, ref Dictionary<Type, IEnumerable<Type>> contractsType)
        {
            BindSelf(bindInformation);
            BindInterfacesTo(bindInformation, ref contractsType);
        }
    }
}