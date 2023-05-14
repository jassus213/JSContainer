using System;

namespace JSContainer.Common.TypeInstancePair
{
    public struct TypeInstancePair
    {
        public readonly Type Type;
        public readonly object Instance;

        public TypeInstancePair(Type type, object instance)
        {
            Type = type;
            Instance = instance;
        }
    }
}