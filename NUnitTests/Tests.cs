using System.Diagnostics;
using JSInjector.Tests;
using NUnit.Framework;
using NUnitTests.Installers;
using NUnitTests.Installers.BindWithArguments;

namespace NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBindInterfacesTo()
        {
            var installer = new BindInterfacesToTestInstaller();
            installer.Install();
        }

        [Test]
        public void BindInterfacesAndSelfTo()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var installer = new BindInterfacesAndSelfToTestInstaller();
            installer.Install();
            stopwatch.Stop();
            Debug.WriteLine("Time to Finish Build " + stopwatch.Elapsed);
        }

        [Test]
        public void BindFactory()
        {
            var installer = new BindFactoryTest();
            installer.Install();
            var factory = installer.Container.Resolve<TestFactory>();
            var result = factory.Create();
        }

        [Test]
        public void BindToTest()
        {
            var installer = new BindTo();
            installer.Install();
        }

        [Test]
        public void BindWithArguments()
        {
            var installer = new BindWithArguments();
            installer.Install();
        }

        [Test]
        public void BindWithArgumentsException()
        {
            var installer = new BindWithArgumentsExceptionInstaller();
            installer.Install(); 
        }
    }
}