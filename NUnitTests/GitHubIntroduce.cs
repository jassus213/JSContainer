using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Bar>();
        Container.BindSelfTo<Test>();
        Container.Initialize();
    }
}