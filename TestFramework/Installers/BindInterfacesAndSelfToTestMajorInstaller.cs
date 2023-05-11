using JSInjector.Binding;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using TestFramework.Entity;

namespace NUnitTests.Installers
{
    public class BindInterfacesAndSelfToTestMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindInterfacesAndSelfTo<TestClass>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Foo>().AsTransient();
            Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
            Container.Initialize();
        }
    }
}