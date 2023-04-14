namespace JSInjector.Contracts
{
    public interface IContainer
    {
        void Bind<T>();
        void GetBind<T>();
    }
}