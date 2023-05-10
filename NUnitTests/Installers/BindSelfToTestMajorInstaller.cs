using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using Test = JSInjector.Tests.Test;

namespace NUnitTests.Installers
{
    public class BindSelfToTestMajorInstaller : MajorInstaller
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
}