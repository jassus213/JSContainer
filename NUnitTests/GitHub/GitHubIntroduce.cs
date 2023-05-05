using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests.GitHub;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindInterfacesAndSelfTo<Bar>();
        Container.BindInterfacesAndSelfTo<Foo>();
        Container.Initialize();
    }
}