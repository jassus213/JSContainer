using System;
using JSContainer.Tests.CircularDependency.EfficiencyTest;
using NUnit.Framework;
using TestFramework.Entity;
using TestFramework.Installers;

namespace TestFramework
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void EffiencyTestCircularDependency()
        {
            var test = new EffiencyTestCircularDependency(new BindInterfacesAndSelfToTestMajorInstaller());
        }

        [Test]
        public void TreeTest()
        {
            var installer = new BindInterfacesAndSelfToTestMajorInstaller();
            installer.Install();

            var bar = installer.Container.Resolve<Bar>();
            var test1 = installer.Container.Resolve<Test1>();

            Assert.AreEqual(bar.Foo, test1.TestScope1.Foo);
            Assert.AreEqual(bar.Foo, test1.Foo);

            var testClass = installer.Container.Resolve<TestClass>();
            var anotherScope = installer.Container.Resolve<AnotherScope>();

            if (testClass.Foo == anotherScope.Foo)
                Console.WriteLine(true);

            Assert.AreEqual(testClass.Foo, anotherScope.Foo);

            var anotherScope2 = installer.Container.Resolve<AnotherScope2>();
            
            Assert.AreNotEqual(anotherScope2.Foo, bar.Foo);
        }
    }
}