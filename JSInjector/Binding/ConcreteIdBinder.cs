using System.Collections.Generic;
using JSInjector.Binding.BindInfo;
using NUnit.Framework;

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

        public void WithArguments<TArg1>(TArg1 argument1)
        {

        }

        public void WithArguments<TArg1, TArg2>(TArg1 argument1)
        {

        }

        public void FromResolve(object obj, BindTypes bindType)
        {
            var type = obj.GetType();
            _diContainer.InitializeFromResolve(type, bindType, new KeyValuePair<bool, object>(true, obj));
        }
    }
}