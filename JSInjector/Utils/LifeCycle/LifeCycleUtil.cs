using System;
using System.Collections.Generic;
using JSInjector.JSExceptions;

namespace JSInjector.Utils.LifeCycle
{
    internal static class LifeCycleUtil
    {
        internal static bool IsScopedInstanced(ref Dictionary<Type, Dictionary<Type, object>> scopedInstances, Type currentType, Type instanceType)
        {
            if (!scopedInstances.ContainsKey(currentType))
                return false;

            if (scopedInstances[currentType].ContainsKey(instanceType) &&
                scopedInstances[currentType][instanceType] != null)
                return true;

            return false;
        }

        internal static bool IsSingletonInstanced(this DiContainer container, Type currentType)
        {
            if (!container.ContainerInfo.ContainsKey(currentType))
                throw JsExceptions.BindException.NotBindedException(currentType);

            return container.ContainerInfo[currentType].Key;
        }
    }
}