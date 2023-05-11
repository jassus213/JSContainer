using JSInjector.Binding;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using JsInjectorTest.Installers.AddizionaleTest;

namespace NUnitTests.Installers.AddizionaleTest
{
    public class MainInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Foo>().AsSingleton();
            AddizionaleInstallerTest.Install(Container);
        }
    }
}