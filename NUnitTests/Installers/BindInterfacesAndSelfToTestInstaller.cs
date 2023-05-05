using JSInjector.Installers;
using JSInjector.Tests;


namespace NUnitTests;

public class BindInterfacesAndSelfToTestInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindInterfacesAndSelfTo<Test>().AsSingleton();
        Container.BindInterfacesAndSelfTo<Test1>().AsSingleton();
        Container.BindInterfacesAndSelfTo<Foo>().AsTransient();
        Container.BindInterfacesAndSelfTo<Bar>().AsScoped();
        Container.Initialize();
    }
}