using JSInjector.Installers;
using JSInjector.Tests;
using JSInjector.Tests.WithArguments;

namespace NUnitTests.Installers.BindWithArguments;

public class BindWithArgumentsExceptionInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<WrongArgumentsCountClass>().WithArguments(new Foo(Container), new Bar(Container));
        Container.Initialize();
    }
}