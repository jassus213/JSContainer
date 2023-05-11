using JSInjector.Binding;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using TestFramework.Entity;

namespace JsInjectorTest.Installers
{
    public class BindTo : MajorInstaller
    {
        public override void Install()
        {
            base.Install(); 
            Container.BindSelfTo<Foo>().AsSingleton();
            Container.Bind<IFoo>().To<Foo>();
            Container.BindSelfTo<Bar>().AsSingleton();
            Container.Bind<IBar>().To<Bar>();
            Container.BindInterfacesAndSelfTo<TestClass>().AsSingleton();
            Container.Initialize();
        }
    }
}