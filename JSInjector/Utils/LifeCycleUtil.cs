using System;
using JSInjector.JSExceptions;

namespace JSInjector.Utils
{
    internal static class LifeCycleUtil
    {
        internal static bool IsScopedInstanced(this DiContainer container, Type currentType, Type instanceType)
        {
            if (!container.ScopedInstance.ContainsKey(currentType))
                return false;

            if (container.ScopedInstance[currentType].ContainsKey(instanceType) &&
                container.ScopedInstance[currentType][instanceType] != null)
                return true;

            return false;
        }

        internal static bool IsSingletonInstanced(this DiContainer container, Type currentType)
        {
            if (!container.ContainerInfo.ContainsKey(currentType))
                JsExceptions.BindException.NotBindedException(currentType);

            return container.ContainerInfo[currentType].Key;
        }
    }
}