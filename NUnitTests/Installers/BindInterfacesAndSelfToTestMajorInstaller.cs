using JSInjector.Binding;
using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;

namespace NUnitTests.Installers
{
    public class BindInterfacesAndSelfToTestMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindInterfacesAndSelfTo<Test>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Foo>().AsTransient();
            Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
            Container.Initialize();
        }
    }
}