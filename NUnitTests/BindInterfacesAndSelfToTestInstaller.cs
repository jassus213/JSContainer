using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class BindInterfacesAndSelfToTestInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindInterfacesAndSelfTo<Test>();
        Container.BindInterfacesAndSelfTo<Bar>();
        Container.TestInitialize();
    }
}