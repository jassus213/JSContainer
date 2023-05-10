namespace JSInjector.Contracts
{
    public interface IInitializer
    {
        void Initialize();
        object InitializeWithOutOrder<TConcrete>();
    }
}