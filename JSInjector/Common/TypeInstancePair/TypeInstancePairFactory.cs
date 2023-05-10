using System;
using System.Collections.Generic;
using System.Linq;

namespace JSInjector.Common.TypeInstancePair
{
    public static class TypeInstancePairFactory
    {
        public static readonly Func<object, TypeInstancePair> CreatePair = (instance) =>
            new TypeInstancePair(instance.GetType(), instance);

        public static readonly Func<Type, object, TypeInstancePair> CreatePairWithCurrentType = (instanceType, instance) =>
            new TypeInstancePair(instanceType, instance);

        public static IReadOnlyCollection<TypeInstancePair> CreatInstancePairs(IReadOnlyCollection<object> instances)
        {
            return instances.Select(x => CreatePair(x)).ToArray();
        }
    }
}