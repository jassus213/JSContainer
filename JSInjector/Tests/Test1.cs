﻿using System.Diagnostics;

namespace JSInjector.Tests
{
    public class Test1
    {
        public readonly IBar Bar;
        public readonly IFoo Foo;
        
        public Test1(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
            
            Debug.WriteLine($"{GetType().Name} : Bar Guid is {Bar.PrintGUID()}, Foo Guid is {Foo.PrintGUID()}");
        }
    }
}