using JSInjector.Binding;
using JSInjector.Contracts;
using JSInjector.Installers;

namespace ConsoleExample.Commands.Installer
{
    public class CommandsInstaller : Installer<CommandsInstaller>
    {
        protected override void InstallBindings(IContainer diContainer)
        {
            diContainer.BindInterfacesAndSelfTo<AddEmployeeCommand>().AsSingleton();
            diContainer.Initialize();
        }
    }
}