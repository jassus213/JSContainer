using System;
using JSContainer.Installers;
using JSContainer.Installers.MajorInstaller;
using JSContainer.Tests;
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