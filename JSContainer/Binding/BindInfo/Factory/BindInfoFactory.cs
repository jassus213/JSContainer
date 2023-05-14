using System;
using JSContainer.Common.Enums;
using JSContainer.Factories;

namespace JSContainer.Binding.BindInfo.Factory
{
    public static class BindInfoFactory
    {
        public static BindInformation Create(Type param1, BindType param2, InstanceType param3, DiContainer param4, LifeCycle param5)
        {
            return new BindInformation(param1, param2, param3, param4, param5);
        }
    }
}