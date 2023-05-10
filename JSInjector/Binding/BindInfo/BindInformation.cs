using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JSInjector.Common.Enums;

namespace JSInjector.Binding.BindInfo
{
    public class BindInformation
    {
        internal readonly DiContainer CurrentContainer;
        internal LifeCycle LifeCycle;
        internal List<ParameterExpression> ParameterExpressions => _parameterExpressions;
        private readonly List<ParameterExpression> _parameterExpressions = new List<ParameterExpression>();
        internal readonly InstanceType InstanceType;
        internal IReadOnlyDictionary<Type, Type> ContractsMap => _contractsMap;
        private readonly Dictionary<Type, Type> _contractsMap = new Dictionary<Type, Type>();
        internal List<Type> ContractsTypes => _contractsType;
        private readonly List<Type> _contractsType = new List<Type>();

        internal readonly Type CurrentType;
        private readonly BindType _bindType;
        public Dictionary<Type, object> ArgumentsMap => _argumentsMap;
        private Dictionary<Type, object> _argumentsMap = new Dictionary<Type, object>();

        public BindInformation(Type currentType, BindType bindType, InstanceType instanceType, DiContainer currentContainer,
            LifeCycle lifeCycle)
        {
            CurrentType = currentType;
            _bindType = bindType;
            InstanceType = instanceType;
            CurrentContainer = currentContainer;
            LifeCycle = lifeCycle;
            Initialize();
        }

        private void Initialize()
        {
            switch (_bindType)
            {
                case BindType.Default:
                    break;
                case BindType.SelfTo:
                    this.BindSelfTo();
                    break;
                case BindType.InterfacesAndSelfTo:
                    this.BindInterfacesAndSelfTo(ref CurrentContainer.ContractsInfo);
                    break;
                case BindType.InterfacesTo:
                    this.BindInterfacesTo(ref CurrentContainer.ContractsInfo);
                    break;
            }
        }
    }
}