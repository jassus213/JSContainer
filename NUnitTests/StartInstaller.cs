using JSInjector.Installers;
using Test = JSInjector.Tests.Test;

namespace NUnitTests;

public class StartInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Test>();
    }
}