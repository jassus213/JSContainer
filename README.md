# JSContainer
## Features
### Injection
+ Constructor injection
+ Field Injection (in the development)
+ Method injection (in the development)

# History
This project started only so that I could study DiContainer in more detail, 
but it has grown into something more. Now it can be used in training and in examples 
for novice developers. I see the most adequate use for it in Universities in my country for training.
		

# Introduction to JSContainer API
## Basics for using this container
At the moment, development is only for console applications, as mentioned above. I plan to develop the project for Web applications later. Why am I saying this, oh yes, there are only Singleton binds for console applications. So keep that in mind.
To make your first installer with your binds, you need to allocate a separate class and inherit the MajorInstaller, as well as override the Install method, there must be a **base.Install();** in the Install()
```c#
using JSContainer.Installers;
namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
    }
}
```
***base.Install(); should be only in your Major Installer. This is necessary so that the Container itself bind in the Container***

In order for the container to be initialized and you can get the necessary dependencies, you need to use such a construction
```c#
using JSContainer.Installers;
namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
	Container.Initialize();
    }
}
```
Due to the fact that we are working with a console application, I have not found a better entry point, except to set it myself at the end of the installer.
Now that we have figured out how to write our first installer, 
## Binding Types
### Bind Self To
is responsible for putting the Foo class in the container, and the container will also get all the necessary dependencies for the Foo class and, if 
they are also in the container, will implement them.

***Container.Bind<Bar>().To<Foo>():***
This line of code is responsible for the rigid implementation of the Bar class into the Foo class.
```c#
using JSContainer.Installers;
using JSContainer.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Foo>();
    }
}
```

What kind of problem can arise with such a method, if we want to get into the class Bar, class Foo in the form of an IFoo interface, this will not happen and we will get an exception

If you want to add a contract for a specific object to get a dependency through an interface, you can use this construction.
```c#
using JSContainer.Installers;
using JSContainer.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Foo>();
        Container.Initialize();
    }
}
```
### Bind To
If you want to explicitly prescribe the interfaces you need for a specific instance, you can use this construction
```c#
using JSContainer.Installers;
using JSContainer.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Foo>();
        Container.Bind<IFoo>().To<Foo>();
        Container.Initialize();
    }
}
```
Now we can get a dependency in the form of IFoo in Bar
```c#
using JSContainer.Tests;

namespace NUnitTests.GitHub;

public class GitHubIntroduceBar
{
    public readonly IFoo Foo;
    
    public GitHubIntroduceBar(IFoo foo)
    {
        Foo = foo;
    }
}
```
What can be an alternative to not specify contracts for so much explicitly?
### Bind Interfaces And Self To
This construct will get all the necessary dependencies for a specific object and install all the inherited interfaces from it, as contracts by which we can get a dependency into another object.
```c#
using JSContainer.Installers;
using JSContainer.Tests;

namespace NUnitTests.GitHub;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindInterfacesAndSelfTo<Bar>();
        Container.BindInterfacesAndSelfTo<Foo>();
        Container.Initialize();
    }
}
```

## Life Cycle
I decided that there was no point in an explanation from me personally, so I'll just share useful articles that helped me deal with this issue.
+ https://www.c-sharpcorner.com/article/dependency-injection-service-lifetimes/
+ https://endjin.com/blog/2022/09/service-lifetimes-in-aspnet-core
+ https://dotnetcorecentral.com/blog/service-lifetime/

## Features
In this container, I have added several non-standard features and am still working on them.  For example :In this container, I have added several non-standard features and am still working on them.  For example :
+ Bind From Resolve
+ Bind With Arguments (In the devolopment)