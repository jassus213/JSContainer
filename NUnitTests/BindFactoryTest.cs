using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class BindFactoryTest : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindFactory<TestFactory, Test>();
        Container.Initialize();
    }
}