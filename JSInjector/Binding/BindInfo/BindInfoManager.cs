using System.Linq;
using JSInjector.Utils;

namespace JSInjector.Binding.BindInfo
{
    internal static class BindInfoManager
    {
        private static void BindSelf(this BindInfo bindInfo, DiContainer container)
        {
            var type = bindInfo.CurrentType;
            var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type);
            var parameters =
                InstanceUtil.ParametersUtil.GetParametersExpression(constructor.GetParameters()
                    .Select(x => x.ParameterType));
            bindInfo.SetParameterExpressions(parameters);
        }

        internal static void BindSelfTo(this BindInfo bindInfo, DiContainer container)
        {
            BindSelf(bindInfo, container);
        }

        internal static void BindInterfacesTo(this BindInfo bindInfo, DiContainer container)
        {
            var contracts = InstanceUtil.ContractsUtil.GetContractsExpression(bindInfo.CurrentType);
            foreach (var contract in contracts)
            {
                container.ContractsInfo.Add(contract, new[] { bindInfo.CurrentType });
            }

            bindInfo.AddContracts(contracts);
        }

        internal static void BindInterfacesAndSelfTo(this BindInfo bindInfo, DiContainer container)
        {
            BindSelf(bindInfo, container);
            BindInterfacesTo(bindInfo, container);
        }
    }
}