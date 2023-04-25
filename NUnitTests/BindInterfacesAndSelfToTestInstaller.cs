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
        Container.BindInterfacesAndSelfTo<Foo>().AsSingleton();
        Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
        Container.Initialize();
    }
}