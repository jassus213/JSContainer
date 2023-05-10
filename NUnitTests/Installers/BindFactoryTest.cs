using System;
using JSInjector.Installers;
using JSInjector.Installers.MajorInstaller;
using JSInjector.Tests;

namespace NUnitTests.Installers
{
    public class BindFactoryTest : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindFactory<TestFactory, Test>();
            Container.Initialize();
        }
    }
}