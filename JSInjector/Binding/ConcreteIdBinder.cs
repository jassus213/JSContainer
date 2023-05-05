﻿using System;
using System.Collections.Generic;
using System.Linq;
using JSInjector.Binding.BindInfo;
using LifeCycle = JSInjector.Binding.BindInfo.LifeCycle;

namespace JSInjector.Binding
{
    public class ConcreteIdBinder<TContract>
    {
        internal readonly DiContainer DiContainer;
        private readonly BindInfo.BindInfo _bindInfo;

        public ConcreteIdBinder(DiContainer diContainer, BindInfo.BindInfo bindInfo)
        {
            DiContainer = diContainer;
            _bindInfo = bindInfo;
        }

        public void To<TConcrete>()
        {
            var type = typeof(TConcrete);
            var contractType = typeof(TContract);
            if (contractType.IsInterface && !DiContainer.BindInfoMap[type].ContractsTypes.Contains(contractType))
            {
                DiContainer.BindInfoMap[type].AddContracts(new[] { contractType });
                if (DiContainer.ContractsInfo.ContainsKey(contractType))
                    DiContainer.ContractsInfo[contractType].ToList().Add(contractType);
                else
                {
                    DiContainer.ContractsInfo.Add(typeof(TContract), new[] { type });
                }
            }
        }

        public ConcreteIdLifeCycle<TContract> WithArguments<TArg1>(TArg1 argument1) where TArg1 : class 
        {
            var type = typeof(TContract);
            var arguments = new object[] { argument1 };
            return BindWithArguments(type, arguments);
        }

        public ConcreteIdLifeCycle<TContract> WithArguments<TArg1, TArg2>(TArg1 argument1, TArg2 argument2) where TArg1 : class where TArg2 : class
        {
            var type = typeof(TContract);
            var arguments = new object[] { argument1, argument2 };
            return BindWithArguments(type, arguments);
        }

        private ConcreteIdLifeCycle<TContract> BindWithArguments(Type type, IReadOnlyCollection<object> arguments)
        {
            DiContainer.BindInfoMap[type].AddArguments(arguments);
            return new ConcreteIdLifeCycle<TContract>(DiContainer);
        }

        public ConcreteIdLifeCycle<TContract> FromResolve(object obj, BindTypes bindType = BindTypes.Default)
        {
            var type = obj.GetType();
            DiContainer.InitializeFromResolve(type, bindType, new KeyValuePair<bool, object>(true, obj),
                LifeCycle.Default);
            return new ConcreteIdLifeCycle<TContract>(DiContainer);
        }
    }
}