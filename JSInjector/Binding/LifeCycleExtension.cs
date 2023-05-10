namespace JSInjector.Binding
{
    public static class LifeCycleExtension
    {
        public static void AsSingleton<TContract>(this ConcreteIdBinder<TContract> concreteIdBinder)
        {
            new ConcreteIdLifeCycle<TContract>(concreteIdBinder.DiContainer).AsSingleton();
        }

        public static void AsTransient<TContract>(this ConcreteIdBinder<TContract> concreteIdBinder)
        {
            new ConcreteIdLifeCycle<TContract>(concreteIdBinder.DiContainer).AsTransient();
        }

        public static void AsScoped<TContract>(this ConcreteIdBinder<TContract> concreteIdBinder)
        {
            new ConcreteIdLifeCycle<TContract>(concreteIdBinder.DiContainer).AsSingleton();
        }
    }
}