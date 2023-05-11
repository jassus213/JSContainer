using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using JSInjector.Tests.WithArguments;

namespace NUnitTests.Installers.BindWithArguments
{
    public class BindWithArgumentsExceptionMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindSelfTo<WrongArgumentsCountClass>().WithArguments(new Foo(Container));
            Container.Initialize();
        }
    }
}