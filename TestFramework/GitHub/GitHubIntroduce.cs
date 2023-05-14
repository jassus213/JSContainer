using JSContainer.Installers;
using JSContainer.Installers.MajorInstaller;
using JSContainer.Tests;
using TestFramework.Entity;

namespace NUnitTests.GitHub
{
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
}