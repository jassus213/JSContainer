using System;
using JSInjector.Factories;

namespace JSInjector.Binding.BindInfo.Factory
{
    public static class BindInfoFactory
    {
        public static BindInfo Create(Type param1, BindTypes param2, InstanceType param3, DiContainer param4, LifeCycle param5)
        {
            return new BindInfo(param1, param2, param3, param4, param5);
        }
    }
}