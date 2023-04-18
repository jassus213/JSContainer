using JSInjector.Contracts;
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
        public void TestBindInterfacesTo()
        {
            var installer = new BindInterfacesToTestInstaller();
            installer.Install();
        }

        [Test]
        public void BindInterfacesAndSelfTo()
        {
            var installer = new BindInterfacesAndSelfToTestInstaller();
            installer.Install();

            IContainer container = installer.Container.Resolve<IContainer>();
        }
    }
}