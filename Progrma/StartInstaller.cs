using System;
using JSInjector.Installers;
using JSInjector.Tests;
using NUnitTests;

namespace Progrma
{
    public class StartInstaller : MajorInstaller
    {
        public override void Install()
        {
            base.Install();
            Container.BindSelfTo<Test>();
            Container.BindSelfTo<Foo>();
            Container.Instantiate(Container);
        }
    }
}