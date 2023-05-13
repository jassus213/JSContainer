using JSInjector.Binding;
using JSInjector.Contracts;
using JSInjector.Installers;

namespace ConsoleExample.Commands.Installer
{
    public class CommandsInstaller : Installer<CommandsInstaller>
    {
        protected override void InstallBindings(IContainer container)
        {
            container.BindInterfacesAndSelfTo<AddEmployeeCommand>().AsSingleton();
            container.BindInterfacesAndSelfTo<FindEmployeeCommand>().AsSingleton();
        }
    }
}