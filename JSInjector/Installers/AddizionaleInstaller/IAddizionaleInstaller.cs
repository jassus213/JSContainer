using JSInjector.Contracts;

namespace JSInjector.Installers.AddizionaleInstaller
{
    public interface IAddizionaleInstaller
    {
        void InstallBindings(IContainer container);
    }
}