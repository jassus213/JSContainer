using JSInjector.Contracts;

namespace JSInjector.Installers.AddizionaleInstaller
{
    public abstract class AddizionaleInstaller
    {
        protected abstract void InstallBindings(IContainer diContainer);
    }
}