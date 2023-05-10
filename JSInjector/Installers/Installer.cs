using JSInjector.Binding;
using JSInjector.Contracts;

namespace JSInjector.Installers
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