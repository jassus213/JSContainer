using JSContainer.Installers;
using JSContainer.Installers.MajorInstaller;
using JSContainer.Tests;
using TestFramework.Entity;
using TestFramework.Entity.WithArguments;

namespace NUnitTests.Installers.BindWithArguments
{
    public class BindWithArgumentsExceptionMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindSelfTo<WrongArgumentsCountClass>().WithArguments(new Foo());
            Container.Initialize();
        }
    }
}