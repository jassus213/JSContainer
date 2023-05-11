using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using TestFramework.Entity;

namespace NUnitTests.Installers
{
    public class BindSelfToTestMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindSelfTo<Bar>();
            Container.BindSelfTo<TestClass>();
            Container.BindSelfTo<Foo>();
            Container.Initialize();
        }
    }
}