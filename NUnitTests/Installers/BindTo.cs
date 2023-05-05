using JSInjector.Binding;
using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests.Installers;

public class BindTo : MajorInstaller
{
    public override void Install()
    {
        base.Install(); 
        Container.BindSelfTo<Foo>().AsSingleton();
        Container.Bind<IFoo>().To<Foo>();
        Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
        Container.BindInterfacesAndSelfTo<Test>().AsSingleton();
        Container.Initialize();
    }
}