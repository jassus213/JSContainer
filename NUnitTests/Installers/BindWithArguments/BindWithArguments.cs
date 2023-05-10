using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using JSInjector.Tests.WithArguments;
using NUnitTests.Installers.AddizionaleTest;

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
            var foo = new Foo(Container);
            Container.BindSelfTo<WithArgumentClass>().WithArguments(foo).AsSingleton();
            Container.Initialize();
        }
    }
}