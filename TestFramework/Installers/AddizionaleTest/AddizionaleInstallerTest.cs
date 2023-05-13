using JSInjector.Binding;
using JSInjector.Contracts;
using JSInjector.Installers;
using JSInjector.Tests;
using TestFramework.Entity;

namespace JsInjectorTest.Installers.AddizionaleTest
{
    public class AddizionaleInstallerTest : Installer<AddizionaleInstallerTest>
    {
        protected override void InstallBindings(IContainer container)
        {
            container.BindInterfacesAndSelfTo<TestClass>().AsSingleton();
            container.Initialize();
        }
    }
}