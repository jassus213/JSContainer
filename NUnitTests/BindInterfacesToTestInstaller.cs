﻿using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class BindInterfacesToTestInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Test>();
        Container.BindSelfTo<Bar>();
        Container.BindSelfTo<Foo>();
    }
}