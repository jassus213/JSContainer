using System.Collections.Generic;
using JSInjector.Binding.BindInfo;
using NUnit.Framework;
using LifeCycle = JSInjector.Binding.BindInfo.LifeCycle;

namespace JSInjector.Binding
{
    public class ConcreteIdBinder<TContract>
    {
        private readonly DiContainer _diContainer;
        private readonly BindInfo.BindInfo _bindInfo;

        public ConcreteIdBinder(DiContainer diContainer, BindInfo.BindInfo bindInfo)
        {
            _diContainer = diContainer;
            _bindInfo = bindInfo;
        }

        public void To<TConcrete>()
        {
            var type = typeof(TConcrete);
            if (!_diContainer.BindInfoMap.ContainsKey(type))
            {
                Assert.Fail(type + "Not Binded");
                return;
            }

            var tConcreteInfo = _bindInfo;
            tConcreteInfo.ContractsTypes.Add(typeof(TContract));
        }

        public ConcreteIdLifeCycle<TContract> WithArguments<TArg1>(TArg1 argument1)
        {
            return new ConcreteIdLifeCycle<TContract>(_diContainer);
        }

        public ConcreteIdLifeCycle<TContract> WithArguments<TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            return new ConcreteIdLifeCycle<TContract>(_diContainer);
        }

        public ConcreteIdLifeCycle<TContract> FromResolve(object obj, BindTypes bindType = BindTypes.Default)
        {
            var type = obj.GetType();
            _diContainer.InitializeFromResolve(type, bindType, new KeyValuePair<bool, object>(true, obj),
                LifeCycle.Default);
            return new ConcreteIdLifeCycle<TContract>(_diContainer);
        }
    }
}