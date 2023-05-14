using JSContainer.Binding;
using JSContainer.Contracts;
using JSContainer.Installers;
using JSContainer.Tests;
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