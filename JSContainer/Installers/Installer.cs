using JSContainer.Contracts;
using JSContainer.Binding;

namespace JSContainer.Installers
{
    public abstract class Installer<TInstaller> : AddizionaleInstaller.AddizionaleInstaller where TInstaller : Installer<TInstaller>
    {
        public static void Install(IContainer container)
        {
            container.BindInterfacesAndSelfTo<TInstaller>().AsSingleton();
            var currentInstaller = (TInstaller)container.InitializeWithOutOrder<TInstaller>();
            currentInstaller.InstallBindings(container);
        }
    }
}