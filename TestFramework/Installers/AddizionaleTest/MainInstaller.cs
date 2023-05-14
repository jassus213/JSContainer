using JSContainer.Binding;
using JSContainer.Installers.MajorInstaller;
using JSContainer.Tests;
using JsInjectorTest.Installers.AddizionaleTest;
using TestFramework.Entity;

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