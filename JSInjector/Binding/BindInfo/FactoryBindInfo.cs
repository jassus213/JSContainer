using System;

namespace JSInjector.Binding.BindInfo
{
    public class FactoryBindInfo
    {
        internal readonly Type FactoryType;
        internal Type RequiredInstance;
        internal int GenericArguments;
        
        public FactoryBindInfo(Type factoryType, Type requiredInstance, bool hasArgs, int genericArguments, Type args = null)
        {
            FactoryType = factoryType;
            RequiredInstance = requiredInstance;
            GenericArguments = genericArguments;
        }
    }
}