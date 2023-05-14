using System;
using System.Collections.Generic;
using System.Linq;

namespace JSContainer.Common.TypeInstancePair
{
    public static class TypeInstancePairFactory
    {
        public static readonly Func<object, TypeInstancePair> CreatePair = (instance) =>
            new TypeInstancePair(instance.GetType(), instance);

        public static readonly Func<Type, object, TypeInstancePair> CreatePairWithCurrentType = (instanceType, instance) =>
            new TypeInstancePair(instanceType, instance);
    }
}