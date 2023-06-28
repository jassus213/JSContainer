using System;
using System.Collections.Generic;
using System.Linq;
using JSContainer.Utils.Instance;
using JSContainer.Utils;

namespace JSContainer.Binding.BindInfo
{
    internal static class BindInfoManager
    {
        private static void BindSelf(this BindInformation bindInformation)
        {
            var type = bindInformation.CurrentType;
            var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type, ConstructorConventionsSequence.First);
            var parameters = constructor.GetParameters();
            bindInformation.AddParameterExpressions(parameters.Select(x => x.ParameterType).ToArray());
        }

        internal static void BindSelfTo(this BindInformation bindInformation)
        {
            BindSelf(bindInformation);
        }

        internal static void BindInterfacesTo(this BindInformation bindInformation,
            ref Dictionary<Type, IEnumerable<Type>> contractsType)
        {
            var contracts = InstanceUtil.ContractsUtil.GetContractsExpression(bindInformation.CurrentType);
            if (contracts.Any())
            {
                foreach (var contract in contracts)
                {
                    if (contractsType.ContainsKey(contract))
                    {
                        var enumerable = contractsType[contract].Append(bindInformation.CurrentType);
                        contractsType[contract] = enumerable;
                    }
                    else
                        contractsType.Add(contract, new[] { bindInformation.CurrentType });
                }

                bindInformation.AddContracts(contracts);
            }
        }

        internal static void BindInterfacesAndSelfTo(this BindInformation bindInformation,
            ref Dictionary<Type, IEnumerable<Type>> contractsType)
        {
            BindSelf(bindInformation);
            BindInterfacesTo(bindInformation, ref contractsType);
        }
    }
}