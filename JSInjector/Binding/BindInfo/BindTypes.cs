namespace JSInjector.Binding.BindInfo
{
    public enum InstanceType
    {
        Default,
        Factory
    }
    
    public enum BindTypes
    {
        Default,
        SelfTo,
        InterfacesTo,
        InterfacesAndSelfTo
    }

    public enum LifeCycle
    {
        Default,
        Singleton,
        Scoped,
        Transient
    }
}