using System;

namespace JSContainer.Common
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
        
    }
}