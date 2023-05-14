using JSContainer.Contracts;

namespace JSContainer.Installers.AddizionaleInstaller
{
    public abstract class AddizionaleInstaller
    {
        protected abstract void InstallBindings(IContainer container);
    }
}