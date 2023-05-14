using JSContainer.Binding;
using JSContainer.Installers.MajorInstaller;
using TestFramework.Entity;

namespace TestFramework.Installers
{
    public class BindInterfacesAndSelfToTestMajorInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindInterfacesAndSelfTo<AnotherScope>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Test1>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Foo>().AsScoped();
            Container.BindInterfacesAndSelfTo<TestClass>().AsSingleton();
            Container.BindInterfacesAndSelfTo<Bar>().AsSingleton();
            Container.BindInterfacesAndSelfTo<TestScope1>().AsSingleton();
            Container.BindInterfacesAndSelfTo<AnotherScope2>().AsSingleton();
            Container.Initialize();
        }
    }
}