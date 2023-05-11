using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using JsInjectorTest.Installers.AddizionaleTest;
using NUnitTests.Installers.AddizionaleTest;
using TestFramework.Entity;
using TestFramework.Entity.WithArguments;

namespace NUnitTests.Installers.BindWithArguments
{
    public class BindWithArguments : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            /*Container.BindInterfacesTo<Bar>();
            Container.BindInterfacesTo<Foo>();*/
            AddizionaleInstallerTest.Install(Container);
            var foo = new Foo();
            Container.BindSelfTo<WithArgumentClass>().WithArguments(foo).AsSingleton();
            Container.Initialize();
        }
    }
}