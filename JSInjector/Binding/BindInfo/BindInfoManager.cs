using System.Linq;
using JSInjector.Utils;

namespace JSInjector.Binding.BindInfo
{
    public class BindInfoManager
    {
        private readonly BindInfo _bindInfo;

        public BindInfoManager(BindInfo bindInfo)
        {
            _bindInfo = bindInfo;
        }

        private void BindSelf(DiContainer container)
        {
            var type = _bindInfo.CurrentType;
            var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type);
            var parameters =
                InstanceUtil.ParametersUtil.GetParametersExpression(constructor.GetParameters()
                    .Select(x => x.ParameterType));
            _bindInfo.SetParameterExpressions(parameters);
        }

        internal void BindSelfTo(DiContainer container)
        {
            BindSelf(container);
        }

        internal void BindInterfacesTo(DiContainer container)
        {
            var contracts = InstanceUtil.ContractsUtil.GetContractsExpression(_bindInfo.CurrentType);
            foreach (var contract in contracts)
            {
                container.ContractsInfo.Add(contract, new[] { _bindInfo.CurrentType });
            }

            _bindInfo.SetContracts(contracts);
        }

        internal void BindInterfacesAndSelfTo(DiContainer container)
        {
            BindSelf(container);
            BindInterfacesTo(container);
        }
    }
}