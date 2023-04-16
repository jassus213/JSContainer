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
        public void Test1()
        {
            var installer = new StartInstaller();
            installer.Install();
            
            var container = installer.Container.Resolve<DiContainer>();
            var test = installer.Container.Resolve<Test>();
            var foo = installer.Container.Resolve<Foo>();
            Assert.AreEqual(container, test.DiContainer);
            Assert.AreEqual(test.DiContainer, foo.DiContainer);
        }

        [Test]
        public void Test2()
        {
            var installer = new StartInstaller();
            installer.Install();
            var test = installer.Container.Resolve<Test>();
        }
    }
}