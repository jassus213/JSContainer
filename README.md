# JSInjector

## Features
### Injection
+ Constructor injection
+ Field Injection (in the development)
+ Method injection (in the development)

# History
This project started only so that I could study DiContainer in more detail, 
but it has grown into something more. Now it can be used in training and in examples 
for novice developers. I see the most adequate use for it in Universities in my country for training.
		

# Introduction to JSInjector API
At the moment, development is only for console applications, as mentioned above. I plan to develop the project for Web applications later. Why am I saying this, oh yes, there are only Singleton binds for console applications. So keep that in mind.
To make your first installer with your binds, you need to allocate a separate class and inherit the MajorInstaller, as well as override the Install method, there must be a **base.Install();** in the Install()
```c#
using JSInjector.Installers;
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

Now that we have figured out how to write our first installer, 
let's get down to the main thing. The code line ***Container.BindSelfTo<Foo>();*** 
is responsible for putting the Foo class in the container, and the container will also get all the necessary dependencies for the Foo class and, if 
they are also in the container, will implement them.

***Container.Bind<Bar>().To<Foo>():***
This line of code is responsible for the rigid implementation of the Bar class into the Foo class.
```c#
using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Foo>();
        Container.Bind<Bar>().To<Foo>();
    }
}
```

Due to the fact that we are working with a console application, I have not found a better entry point, except to set it myself at the end of the installer.
```c#
using JSInjector.Installers;
using JSInjector.Tests;

namespace NUnitTests;

public class GitHubIntroduce : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        Container.BindSelfTo<Foo>();
        Container.Bind<Bar>().To<Foo>();
        Container.Initialize();
    }
}
```
