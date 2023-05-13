using System;
using JSInjector.Tests.CircularDependency.EfficiencyTest;
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
            
            if (bar.Foo == test1.TestScope1.Foo && bar.Foo == test1.Foo)
                Console.WriteLine(true);

            var testClass = installer.Container.Resolve<TestClass>();
            var anotherScope = installer.Container.Resolve<AnotherScope>();
            
            if (testClass.Foo == anotherScope.Foo)
                Console.WriteLine(true);
        }
    }
}