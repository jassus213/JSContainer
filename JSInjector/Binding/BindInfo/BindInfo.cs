using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JSInjector.Binding.BindInfo
{
    public class BindInfo
    {
        private readonly DiContainer _currentContainer;
        internal LifeCycle LifeCycle;
        internal List<ParameterExpression> ParameterExpressions => _parameterExpressions;
        internal readonly InstanceType InstanceType;
        internal readonly BindTypes BindType;
        internal readonly List<Type> ContractsTypes = new List<Type>();
        internal readonly Type CurrentType;
        private readonly List<ParameterExpression> _parameterExpressions = new List<ParameterExpression>();
        private readonly BindInfoManager _bindInfoManager;

        public BindInfo(Type currentType, BindTypes bindType, InstanceType instanceType, DiContainer currentContainer,
            LifeCycle lifeCycle)
        {
            CurrentType = currentType;
            BindType = bindType;
            InstanceType = instanceType;
            _currentContainer = currentContainer;
            LifeCycle = lifeCycle;
            _bindInfoManager = new BindInfoManager(this);
            Initialize();
        }

        private void Initialize()
        {
            switch (BindType)
            {
                case BindTypes.Default:
                    break;
                case BindTypes.SelfTo:
                    _bindInfoManager.BindSelfTo(_currentContainer);
                    break;
                case BindTypes.InterfacesAndSelfTo:
                    _bindInfoManager.BindInterfacesAndSelfTo(_currentContainer);
                    break;
                case BindTypes.InterfacesTo:
                    _bindInfoManager.BindInterfacesTo(_currentContainer);
                    break;
            }
        }

        internal void SetParameterExpressions(IEnumerable<ParameterExpression> parameterExpressions)
        {
            _parameterExpressions.AddRange(parameterExpressions);
        }

        internal void SetContracts(IEnumerable<Type> contracts)
        {
            ContractsTypes.AddRange(contracts);
        }
    }
}