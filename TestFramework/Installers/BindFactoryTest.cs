using System;
using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;
using TestFramework.Entity;

namespace NUnitTests.Installers
{
    public class BindFactoryTest : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindFactory<TestFactory, TestClass>();
            Container.Initialize();
        }
    }
}