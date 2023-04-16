using System.Collections.Generic;
using NUnit.Framework;

namespace JSInjector.Binding
{
    public class ConcreteIdBinder<TContract>
    {
        private readonly DiContainer _diContainer;
        private readonly BindInfo _bindInfo;
        private readonly ObjCreator _objCreator;
        
        public ConcreteIdBinder(DiContainer diContainer, BindInfo bindInfo, ObjCreator objCreator)
        {
            _diContainer = diContainer;
            _bindInfo = bindInfo;
            _objCreator = objCreator;
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
            tConcreteInfo.TypesMap[type].Add(typeof(TContract));
            tConcreteInfo.ContractsTypes.Add(typeof(TContract));
        }

        public void FromResolve(object obj)
        {
            var type = obj.GetType();
            if (_diContainer.ContainerInfo.ContainsKey(type))
                _diContainer.ContainerInfo.Remove(type);
            _diContainer.ContainerInfo.Add(type, new KeyValuePair<bool, object>(true, obj));
        }
    }
}