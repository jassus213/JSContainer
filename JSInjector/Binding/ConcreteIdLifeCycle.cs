using System;
using System.Collections.Generic;
using JSInjector.Binding.BindInfo;

namespace JSInjector.Binding
{
    public class ConcreteIdLifeCycle<TConcrete>
    {
        private readonly DiContainer _diContainer;

        public ConcreteIdLifeCycle(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void AsSingleton()
        {
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeCycle = LifeCycle.Singleton;
        }

        public void AsTransient()
        {
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeCycle = LifeCycle.Transient;
        }

        public void AsScoped()
        {
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeCycle = LifeCycle.Scoped;
            _diContainer.ScopedInstance.Add(typeof(TConcrete), new Dictionary<Type, object>());
        }
    }
}