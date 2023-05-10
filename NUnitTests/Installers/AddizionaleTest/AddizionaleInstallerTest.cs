using JSInjector.Binding;
using JSInjector.Contracts;
using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests.Installers.AddizionaleTest
{
    public class AddizionaleInstallerTest : Installer<AddizionaleInstallerTest>
    {
        protected override void InstallBindings(IContainer diContainer)
        {
            diContainer.BindInterfacesAndSelfTo<Test>().AsSingleton();
            diContainer.Initialize();
        }
    }
}