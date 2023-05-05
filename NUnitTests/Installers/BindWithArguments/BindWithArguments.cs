using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests.Installers;

public class BindWithArguments : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
        Container.BindSelfTo<WithArgumentClass>().WithArguments(new Foo(Container)).AsSingleton();
        Container.Initialize();
    }
}