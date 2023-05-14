using ConsoleExample.Commands.Installer;
using ConsoleExample.Services.Employee;
using JSContainer.Binding;
using JSContainer.Installers.MajorInstaller;

namespace ConsoleExample.Services.Installers
{
    public class MainInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindInterfacesAndSelfTo<EmployeeReader>().AsSingleton();
            Container.BindInterfacesAndSelfTo<EmployeeWriter>().AsSingleton();
            CommandsInstaller.Install(Container);
        }
    }
}