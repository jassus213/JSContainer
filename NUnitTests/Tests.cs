using System.Diagnostics;
using NUnit.Framework;
using NUnitTests.Installers;
using NUnitTests.Installers.AddizionaleTest;
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
        public void BindInterfacesAndSelfTo()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var installer = new BindInterfacesAndSelfToTestMajorInstaller();
            installer.Install();
            stopwatch.Stop();
            Debug.WriteLine("Time to Finish Build " + stopwatch.Elapsed);
        }

        [Test]
        public void BindFactory()
        {
            var installer = new BindFactoryTest();
            installer.Install();
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
            var installer = new BindWithArgumentsExceptionMajorInstaller();
            installer.Install(); 
        }

        [Test]
        public void BindWithAddizionaleInstaller()
        {
            var installer = new MainInstaller();
            installer.Install();
        }
    }
}