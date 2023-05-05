using JSInjector.Installers;
using JSInjector.Tests;
using JSInjector.Tests.WithArguments;

namespace NUnitTests.Installers.BindWithArguments;

public class BindWithArguments : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        /*Container.BindInterfacesTo<Bar>();
        Container.BindInterfacesTo<Foo>();*/
        var bar = new Bar(Container);
        var foo = new Foo(Container);
        Container.BindSelfTo<WithArgumentClass>().WithArguments((IBar)bar, foo).AsSingleton();
        Container.Initialize();
    }
}