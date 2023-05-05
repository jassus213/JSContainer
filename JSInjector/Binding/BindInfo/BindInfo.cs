using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JSInjector.JSExceptions;
using JSInjector.Utils;

namespace JSInjector.Binding.BindInfo
{
    public class BindInfo
    {
        private readonly DiContainer _currentContainer;
        internal LifeCycle LifeCycle;
        internal List<ParameterExpression> ParameterExpressions => _parameterExpressions;
        private readonly List<ParameterExpression> _parameterExpressions = new List<ParameterExpression>();
        internal readonly InstanceType InstanceType;
        internal IReadOnlyDictionary<Type, Type> ContractsMap => _contractsMap;
        private readonly Dictionary<Type, Type> _contractsMap = new Dictionary<Type, Type>();
        internal List<Type> ContractsTypes => _contractTypes;
        private readonly List<Type> _contractTypes = new List<Type>();
        internal readonly Type CurrentType;
        private readonly BindTypes _bindType;
        public IReadOnlyDictionary<Type, object> ArgumentsMap => _argumentsMap;
        private readonly Dictionary<Type, object> _argumentsMap = new Dictionary<Type, object>();

        public BindInfo(Type currentType, BindTypes bindType, InstanceType instanceType, DiContainer currentContainer,
            LifeCycle lifeCycle)
        {
            CurrentType = currentType;
            _bindType = bindType;
            InstanceType = instanceType;
            _currentContainer = currentContainer;
            LifeCycle = lifeCycle;
            Initialize();
        }

        private void Initialize()
        {
            switch (_bindType)
            {
                case BindTypes.Default:
                    break;
                case BindTypes.SelfTo:
                    this.BindSelfTo(_currentContainer);
                    break;
                case BindTypes.InterfacesAndSelfTo:
                    this.BindInterfacesAndSelfTo(_currentContainer);
                    break;
                case BindTypes.InterfacesTo:
                    this.BindInterfacesTo(_currentContainer);
                    break;
            }
        }

        internal void AddArguments(IReadOnlyCollection<object> arguments)
        {
            if (arguments.Count > _parameterExpressions.Count)
                JsExceptions.ConstructorException.IsWrongParamsCountException(CurrentType, _parameterExpressions.Count);
            
            var argumentsTypes = arguments.Select(x => x.GetType()).ToArray();

            foreach (var parameter in _parameterExpressions)
            {
                var parameterType = parameter.Type;

                if (InstanceUtil.IsInterfaceAndBinded(parameter.Type, _currentContainer) &&
                    _currentContainer.ContractsInfo[parameterType].Last() != null)
                    parameterType = _currentContainer.ContractsInfo[parameterType].Last();
                else if (parameter.Type.IsInterface)
                {
                    parameterType = parameter.Type;
                }
                
                _argumentsMap.Add(parameterType, argumentsTypes.First(x => parameterType.IsAssignableFrom(x)));
            }
        }
    }
}