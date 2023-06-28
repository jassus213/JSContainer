using System;
using System.Collections.Generic;
using JSContainer.Common.Enums;
using JSContainer.Binding.BindInfo;

namespace JSContainer.Binding
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
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeTime = LifeTime.Singleton;
        }

        public void AsTransient()
        {
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeTime = LifeTime.Transient;
        }

        public void AsScoped()
        {
            _diContainer.BindInfoMap[typeof(TConcrete)].LifeTime = LifeTime.Scoped;
        }
    }
}