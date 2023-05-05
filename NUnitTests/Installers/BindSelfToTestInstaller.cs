using JSInjector.Installers;
using JSInjector.Tests;
using Test = JSInjector.Tests.Test;

namespace NUnitTests;

public class BindSelfToTestInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Bar>();
        Container.BindSelfTo<Test>();
        Container.BindSelfTo<Foo>();
        Container.Initialize();
    }
}