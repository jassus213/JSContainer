# JS Container Documentation
## Introduction
This documentation provides an overview of the DI (Dependency Injection) Container, its functionality, and how to use it effectively in your application. The DI Container simplifies the management and resolution of dependencies within your codebase.


## Map
- ### Lets Start
  	- [Insatllers](#installers)
  		- [Major Installer](#majorinstaller)
  	   		- [Creating the Main Installer](#majorinstallercreating)
  	       	- [Installing the Main Installer](#majorinstallerinstalling)
  	       	- [Usage and Benefits](#majorinstallerusages)
  	       	- [Example Usages](#majorinstallerexample)
  	        	
  		- [Addizionale Installer](#addizionaleinstaller)
  	   		- [Creating the Addizionale Installer](#addizionaleinstallercreating)
  	       	- [Usage and Benefits](#addizionaleinstallerusages)
- ### Dependency Injection
  	- [Register Services](#registration)
  	  	- [Bind To](#bindto)
  	  	  	- [Syntax](#bindtosyntax)
  	  	  	- [Usage](#bindtousage)
  	  	- [BindInterfacesTo](#bindinterfacesto)
  	  	  	- [Syntax](#bindinterfacestosyntax)
  	  	  	- [Usage](#bindinterfacestousage)
  	  	- [BindInterfacesAndSelfTo](#bindinterfacesandselfto)
  	  	  	- [Syntax](#bindinterfacesandselftosyntax)
  	  	  	- [Usage](#bindinterfacesandselftousage)
  	  	- [BindSelfTo](#bindselfto)
  	  	  	- [Syntax](#bindselftosyntax)
  	  	  	- [Usage](#bindselftousage)
  	- [Signleton](#singleton)
  	  	- [About](#singletonabout)
  		- [Register Singleton](#singletonregister)
  	 - [Scope](#scope)
  	   	- [About](#scopeabout)
  		- [Register Scope](#scoperegister)
  	 - [Transient](#transient)
  	   	- [About](#transientabout)
  		- [Register Transient](#transientregister)
- ### Getting Dependencies from the Container
  	- [Resolving](#resolving)
  	  	- [Resolve](#resolve)
  	  	- [ResolveAll](#resolveall)



# <a id="installers"></a> Installers
## <a id="majorinstaller"></a> Major Installer
The ***MainInstaller*** is a class responsible for installing the main dependencies in the DI Container. It serves as the entry point for registering the core dependencies of your application. By organizing your main dependencies into a dedicated installer class, you can maintain a clean and modular code structure.
### <a id="majorinstallercreating"></a> Creating the Main Installer
To create a ***MainInstaller***, you need to create a new class that extends the ***MajorInstaller*** base class and overrides the ***Install*** method. Here's an example of creating a ***MainInstaller***:
```c#
public class MainInstaller : MajorInstaller
{
    public override void Install()
    {
        base.Install();
        
        // Register main dependencies here
        Container.Bind<IService>().To<DefaultService>();
        Container.Bind<IRepository>().To<DefaultRepository>();
        // ... additional bindings
    }
}
```
In the example above, the MainInstaller class extends MajorInstaller and overrides the Install method. Inside this method, you can register your main dependencies by using the container's binding methods, such as Bind<TValue>().To<TService>().

### <a id="majorinstallerinstalling"></a> Installing the Main Installer
To install the main dependencies from the ***MainInstaller***, you need to create an instance of the installer class and call its ***Install*** method, passing in the container instance. Here's an example of installing the ***MainInstaller***:
```c#
var container = new DIContainer();
var mainInstaller = new MainInstaller();
mainInstaller.Install(container);
```
In the example above, the MainInstaller is installed by creating an instance of the installer class and calling its Install method, passing in the DIContainer instance. This will execute the Install method in the MainInstaller class and register the main dependencies in the container.

## <a id="majorinstallerusages"></a> Usage and Benefits
### 1. Modularity:
By encapsulating the registration of main dependencies into a dedicated installer class, you can achieve modularity and separation of concerns in your application. Each installer can be responsible for a specific set of dependencies, making it easier to maintain and extend your application's dependency graph.
### 2. Code Organization:
The ***MainInstaller*** allows you to keep your dependency registration code organized and structured. By grouping the core dependencies together in a single installer, you improve code readability and maintainability.
### 3. Configuration::
The ***AddizionaleInstaller*** The ***MainInstaller*** provides a central place to configure and register the main dependencies of your application. You can easily modify the installer class to add, remove, or update the registered dependencies, enabling flexible configuration of your application's dependency graph.
### 4. Integration:
The ***MainInstaller*** can be combined with other installers, including additional installers, to compose a complete set of dependencies for your application. This allows you to modularize and configure your dependencies according to your specific needs.

## <a id="majorinstallerexample"></a> Example Usage
Here's an example that demonstrates the usage of the ***MainInstaller***:
```c#
// Step 1: Create an instance of the DI Container
var container = new DIContainer();

// Step 2: Install the MainInstaller
var mainInstaller = new MainInstaller();
mainInstaller.Install(container);
```
In this example, the DI Container is instantiated, and the MainInstaller is installed by creating an instance of the installer class and calling its Install method. After installation, the IService and IRepository dependencies can be resolved using the container.

Please note that the provided code snippets are simplified examples and may need adaptation based on your specific use case and framework.


## <a id="addizionaleinstaller"></a> Addizionale Installer
The **AddizionaleInstaller** is a class responsible for installing additional bindings in the DI Container. It allows you to encapsulate and organize the registration of specific dependencies into a separate installer class. This can be helpful for modularizing your application's dependencies and promoting a clean and organized code structure.
***<a id="addizionaleinstallercreating"/> Creating the Addizionale Installer***
To create an **AddizionaleInstaller**, you need to create a new class that inherits from the **Installer<T>** base class. The generic type parameter **T** should be the derived installer class itself, ensuring that the installer class can be self-installed. Here's an example of creating an **AddizionaleInstaller**:
```c#
public class AddizionaleInstaller : Installer<AddizionaleInstaller>
{
    protected override void InstallBindings(IContainer container)
    {
        // Register additional dependencies here
        container.Bind<IService>().To<CustomService>();
        container.Bind<IRepository>().To<CustomRepository>();
        // ... additional bindings
    }
}
```
In the example above, the **AddizionaleInstaller** class extends **Installer<AddizionaleInstaller>** and overrides the **InstallBindings** method. Inside this method, you can register additional dependencies by using the container's binding methods, such as **Bind<TValue>().To<TService>()**.
## <a id="addizionaleinstallerusages"></a> Usage and Benefits
### 1. Modularity:
By encapsulating the registration of additional dependencies into a separate installer class, you can achieve modularity and separation of concerns in your application. Each installer can be responsible for a specific set of dependencies, making it easier to maintain and extend your application's dependency graph.
### 2. Code Organization:
The ***AddizionaleInstaller*** allows you to keep your dependency registration code organized and structured. By grouping related dependencies together in separate installers, you improve code readability and maintainability.
### 3. Reusability:
The ***AddizionaleInstaller*** can be reused across different projects or modules that require the same additional dependencies. You can easily share and integrate the installer class into different applications, promoting code reuse and reducing duplication.
### 4. Flexability:
The ***AddizionaleInstaller*** can be combined with other installers, including the main installer, to compose a complete set of dependencies for your application. This allows you to modularize and configure your dependencies according to your specific needs.

## <a id="registration"></a> Registration Services
## <a id="bindto"></a> Bind To
The ***Bind.To*** functionality in the DI Container allows you to establish a binding between an interface or a class and a specific implementation. It specifies which concrete implementation should be provided when a dependency is resolved for a given interface or class.
### <a id="bindtosyntax"></a> Syntax
The syntax for using the Bind.To functionality is as follows:
```c#
container.Bind<TInterface>().To<TImplementation>();
```
### <a id="bindtousage"></a> Usage
The ***Bind.To*** functionality is used when you want to explicitly specify the implementation that should be used for a given interface or class. It allows you to decouple the interface or class from its concrete implementation, enabling flexibility and easier maintenance of your codebase.
Binding an Interface
When binding an interface to an implementation, the Bind.To functionality allows you to define the specific implementation that should be used when resolving dependencies for that interface. Here's an example:
```c#
public interface IService
{
    void DoSomething();
}

public class ServiceImplementation : IService
{
    public void DoSomething()
    {
        // Implementation code here
    }
}

// Binding the IService interface to the ServiceImplementation class
container.Bind<IService>().To<ServiceImplementation>();
```
In this example, the IService interface is bound to the ServiceImplementation class using Bind.To. This means that whenever the IService dependency is resolved, an instance of the ServiceImplementation class will be provided.

## <a id="bindinterfacesto"></a> Bind Interfaces To
The BindInterfacesTo functionality in the DI Container allows you to bind a class to multiple interfaces. It specifies that a single concrete implementation should be provided when any of the specified interfaces are resolved.
### <a id="bindinterfacestosyntax"></a> Syntax
The syntax for using the BindInterfacesTo functionality is as follows:
```c#
container.BindInterfacesTo<TImplementation>();
```
### <a id="bindinterfacestousage"></a> Usage
The BindInterfacesTo functionality is used when you want to bind a class to multiple interfaces. This is particularly useful when you have a class that implements multiple interfaces and you want to provide the same implementation for all of them.

Here's an example to illustrate its usage:
```c#
public interface IServiceA
{
    void MethodA();
}

public interface IServiceB
{
    void MethodB();
}

public class ServiceImplementation : IServiceA, IServiceB
{
    public void MethodA()
    {
        // Implementation code for MethodA
    }

    public void MethodB()
    {
        // Implementation code for MethodB
    }
}

// Binding the ServiceImplementation class to both IServiceA and IServiceB
container.BindInterfacesTo<ServiceImplementation>();
```

In this example, the ServiceImplementation class implements both the IServiceA and IServiceB interfaces. By using BindInterfacesTo, the container is configured to provide an instance of ServiceImplementation whenever either IServiceA or IServiceB is resolved.

### <a id="bindinterfacesandselfto"></a> Bind Interfaces And Self To
The BindInterfacesAndSelfTo functionality in the DI Container allows you to bind a class to both its interfaces and itself. It specifies that a single concrete implementation should be provided when any of the specified interfaces or the class itself is resolved.
## <a id="bindinterfacesandselftosyntax"></a> Syntax
The syntax for using the BindInterfacesAndSelfTo functionality is as follows:
```c#
container.BindInterfacesAndSelfTo<TImplementation>();
```
## <a id="bindinterfacesandselftousage"></a> Usage
The BindInterfacesAndSelfTo functionality is used when you want to bind a class to its interfaces and itself. This is particularly useful when you have a class that both implements an interface and requires direct resolution.
Here's an example to illustrate its usage:
```c#
public interface IService
{
    void DoSomething();
}

public class ServiceImplementation : IService
{
    public void DoSomething()
    {
        // Implementation code here
    }
}

// Binding the ServiceImplementation class to both IService and itself
container.BindInterfacesAndSelfTo<ServiceImplementation>();
```
In this example, the ServiceImplementation class implements the IService interface. By using BindInterfacesAndSelfTo, the container is configured to provide an instance of ServiceImplementation whenever either IService or ServiceImplementation is resolved.

### <a id="bindselfto"></a> Bind Self To
The BindSelfTo functionality in the DI Container allows you to bind a class to itself. It specifies that a single concrete implementation should be provided when the class itself is resolved.
## <a id="bindselftosyntax"></a> Syntax
The syntax for using the BindSelfTo functionality is as follows:
```c#
container.BindSelfTo<TImplementation>();
```
In this case, TImplementation represents the concrete implementation that will be provided when the class itself is resolved.

## <a id="bindselftousage"></a> Usage
The BindSelfTo functionality is used when you want to explicitly specify the implementation that should be used for a class itself. This is particularly useful when you have a class that doesn't implement any interfaces but still needs to be resolved directly.

Here's an example to illustrate its usage:
```c#
public class MyService
{
    public void DoSomething()
    {
        // Implementation code here
    }
}

// Binding the MyService class to itself
container.BindSelfTo<MyService>();
```
In this example, the MyService class is bound to itself using BindSelfTo. This means that whenever the MyService class itself is resolved, an instance of MyService will be provided.
