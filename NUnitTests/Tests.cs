using JSInjector;
using JSInjector.Tests;
using NUnit.Framework;

namespace NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        

        [Test]
        public void TestBindSelf()
        {
            var installer = new BindSelfToTestInstaller();
            installer.Install();
            var test = installer.Container.Resolve<Test>();
            var foo = installer.Container.Resolve<Foo>();
            var bar = installer.Container.Resolve<Bar>();
            var container = installer.Container.Resolve<DiContainer>();
            Assert.AreEqual(test.DiContainer, container);
            Assert.AreEqual(container, foo.DiContainer);
        }

        [Test]
        public void TestBindInterfacesTo()
        {
            var installer = new BindInterfacesToTestInstaller();
            installer.Install();
        }
    }
}