using System;
using JSInjector.Tests.CircularDependency.EfficiencyTest;
using NUnit.Framework;
using NUnitTests.Installers;

namespace JsInjectorTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void EffiencyTestCircularDependency()
        {
            var test = new EffiencyTestCd(new BindInterfacesAndSelfToTestMajorInstaller());
        }
    }
}