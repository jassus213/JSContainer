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
  	       - [Installing the Addizionale Installer](#addizionaleinstallerinstalling)
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
  	   	- [RegisterTransient](#transientregister)
- ### Getting Dependencies from the Container
  	- [Resolving](#resolving)
  	  	- [Resolve](#resolve)
  	  	- [ResolveAll](#resolveall)



# <a id="installers"/> Installers
## <a id="majorinstaller"/> Major Installer
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

### <a id="majorinstallerusages"></a> Usage and Benefits
#### 1. Modularity:
By encapsulating the registration of main dependencies into a dedicated installer class, you can achieve modularity and separation of concerns in your application. Each installer can be responsible for a specific set of dependencies, making it easier to maintain and extend your application's dependency graph.
#### 2. Code Organization:
The ***MainInstaller*** allows you to keep your dependency registration code organized and structured. By grouping the core dependencies together in a single installer, you improve code readability and maintainability.
#### 3. Configuration::
The ***AddizionaleInstaller*** The ***MainInstaller*** provides a central place to configure and register the main dependencies of your application. You can easily modify the installer class to add, remove, or update the registered dependencies, enabling flexible configuration of your application's dependency graph.
#### 4. Integration:
The ***MainInstaller*** can be combined with other installers, including additional installers, to compose a complete set of dependencies for your application. This allows you to modularize and configure your dependencies according to your specific needs.

### <a id="majorinstallerexample"></a> Example Usage
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


## <a id="addizionaleinstaller"/> Addizionale Installer
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
### <a id="addizionaleinstallerinstalling"> Installing
To use the AddizionaleInstaller, you need to create a new class that inherits from Installer<T> and implement the InstallBindings method. Here's an example:
```c#
public class AddizionaleInstaller : Installer<AddizionaleInstaller>
{
    protected override void InstallBindings(IContainer container)
    {
        // Register additional dependencies using container.Bind() statements
        container.Bind<IMyDependency>().To<MyDependency>().AsSingleton();
        container.Bind<IOtherDependency>().To<OtherDependency>().AsTransient();
        
        // You can also call other installers here if needed
        SomeOtherInstaller.Install(container);
    }
}
```
In the InstallBindings method, you can use the container.Bind() statements to register additional dependencies. These dependencies can be registered using any of the supported lifecycle options (Singleton, Transient, Scoped) as per your requirements. Additionally, you can call other installers within the InstallBindings method if needed.
### Calling AddizionaleInstaller from other installers
To call the AddizionaleInstaller from other installers, you can simply invoke the Install method of the AddizionaleInstaller class and pass the container instance as a parameter. Here's an example:
```c#
public class MainInstaller : Installer<MainInstaller>
{
    public override void Install()
    {
        base.Install();
        
        // Register primary dependencies using container.Bind() statements
        
        // Call AddizionaleInstaller and pass the container instance
        AddizionaleInstaller.Install(Container);
        
        // Continue registering other dependencies or installers
    }
}
```
In the Install method of your main installer class (e.g., MainInstaller), you can call the Install method of the AddizionaleInstaller by invoking AddizionaleInstaller.Install(Container), where Container is the instance of your DI container.

By calling AddizionaleInstaller.Install(Container), the InstallBindings method of the AddizionaleInstaller will be executed, and any additional dependencies specified in that installer will be registered.

### <a id="addizionaleinstallerusages"/> Usage and Benefits
#### 1. Modularity:
By encapsulating the registration of additional dependencies into a separate installer class, you can achieve modularity and separation of concerns in your application. Each installer can be responsible for a specific set of dependencies, making it easier to maintain and extend your application's dependency graph.
#### 2. Code Organization:
The ***AddizionaleInstaller*** allows you to keep your dependency registration code organized and structured. By grouping related dependencies together in separate installers, you improve code readability and maintainability.
#### 3. Reusability:
The ***AddizionaleInstaller*** can be reused across different projects or modules that require the same additional dependencies. You can easily share and integrate the installer class into different applications, promoting code reuse and reducing duplication.
#### 4. Flexability:
The ***AddizionaleInstaller*** can be combined with other installers, including the main installer, to compose a complete set of dependencies for your application. This allows you to modularize and configure your dependencies according to your specific needs.

## <a id="registration"></a> Registration Services
Registration Services chapter covers the various ways to register services and dependencies in a dependency injection (DI) container. It explains the different registration methods and their corresponding usage scenarios.
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
#### <a id="bindinterfacestousage"></a> Usage
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

## <a id="bindinterfacesandselfto"></a> Bind Interfaces And Self To
The BindInterfacesAndSelfTo functionality in the DI Container allows you to bind a class to both its interfaces and itself. It specifies that a single concrete implementation should be provided when any of the specified interfaces or the class itself is resolved.
### <a id="bindinterfacesandselftosyntax"></a> Syntax
The syntax for using the BindInterfacesAndSelfTo functionality is as follows:
```c#
container.BindInterfacesAndSelfTo<TImplementation>();
```
### <a id="bindinterfacesandselftousage"></a> Usage
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

## <a id="bindselfto"></a> Bind Self To
The BindSelfTo functionality in the DI Container allows you to bind a class to itself. It specifies that a single concrete implementation should be provided when the class itself is resolved.
### <a id="bindselftosyntax"></a> Syntax
The syntax for using the BindSelfTo functionality is as follows:
```c#
container.BindSelfTo<TImplementation>();
```
In this case, TImplementation represents the concrete implementation that will be provided when the class itself is resolved.

### <a id="bindselftousage"></a> Usage
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

## <a id="singleton"/> Singleton
Registering a dependency as a Singleton ensures that only a single instance of the implementation is created and shared throughout the application. This is useful when you want to maintain a single shared state or ensure that there is only one instance of a particular service.
### <a id="singletonabout"/> About
Singleton registration allows you to ensure that only one instance of a dependency is created and shared across the application. Whether you're registering a class itself or an interface with its implementation, Singletons offer shared state, performance benefits, and consistency in dependency resolution. However, make sure to handle thread safety, lifetime management, and consider the overall dependency graph when using Singletons in your application.
### <a id="singletonregister"/> Register Singleton
The BindSelfTo<TService>().AsSingleton() syntax allows you to register a class as a Singleton, where both the interface and the implementation are the same.
```c#
container.BindSelfTo<MyService>().AsSingleton();
```
In this example, the MyService class is registered as a Singleton using BindSelfTo<TService>().AsSingleton(). This means that every time MyService is resolved, the same instance will be returned.

## <a id="scope"/> Scope
Registering a dependency as Scoped means that a new instance of the implementation will be created once per scope. The scope defines a specific context or boundary within which the dependency instance will be shared.
### <a id="scopeabout"> About
Scoped registration allows you to create and share a single instance of a dependency within a specific scope. It provides controlled instance sharing, scope-based lifetime management, and performance benefits compared to Transient dependencies. Proper scope management and thread safety considerations should be taken into account
### <a id="scoperegister"/> Register Scope
The BindSelfTo<TService>().AsScope() syntax allows you to register a class as a scoped instance, where both the interface and the implementation are the same.
```c#
// Example 1: Registering a class as Scoped
container.BindSelfTo<MyService>().AsScoped();

// Example 2: Registering an interface and implementation as Scoped
container.Bind<IMyService>().To<MyService>().AsScoped();
```
In this example, the MyService class is registered as a scoped instance using BindSelfTo<TService>().InScope(). This means that a new instance of MyService will be created for each scope.

## <a id="transient"> Transient
Registering a dependency as Transient means that a new instance of the implementation will be created every time it is resolved. This is useful when you want to have a fresh instance of the dependency for each request or usage.
### <a id="transientabout"> About
Transient registration allows you to create and resolve a new instance of a dependency each time it is requested. It offers fresh state, flexibility, and scalability in dependency resolution. Consider the performance impact and manage the dependency graph appropriately when using Transient dependencies in your application.
### <a id="transientregister"> Register Transient
The Bind<TInterface>().To<TImplementation>().AsTransient() syntax allows you to register an interface and its implementation as Transient.
```c#
// Example 1: Registering a class as Transient
container.BindSelfTo<MyService>().AsTransient();

// Example 2: Registering an interface and implementation as Transient
container.Bind<IMyService>().To<MyService>().AsTransient();
```
In this example, the IMyService interface is registered with its implementation MyService as Transient using Bind<TInterface>().To<TImplementation>().AsTransient(). This ensures that whenever IMyService is resolved, a new instance of MyService will be created.
