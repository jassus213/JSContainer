namespace JSContainer.Contracts
{
    public interface IInitializer
    {
        void Initialize();
        object InitializeWithOutOrder<TConcrete>();
    }
}