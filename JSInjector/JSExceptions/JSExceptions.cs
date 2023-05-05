using System;
using NUnit.Framework;

namespace JSInjector.JSExceptions
{
    public static class JsExceptions
    {
        public static class BindException
        {
            public static void NotBindedException(Type type)
            {
                var message = $"{nameof(type)} Is not binded";
                Assert.Fail(message);
            }

            public static void AlreadyBindedException(Type type)
            {
                var message = $"{nameof(type)} Already Binded";
                Assert.Fail(message);
            }

            public static void CircularDependency(Type instanceType,
                Type parameterType)
            {
                var message = $"Circular Dependency {nameof(instanceType)} and {nameof(parameterType)}";
                Assert.Fail(message);
            }

            public static void ContractNotBindedToInstance(Type instanceType, Type contractType)
            {
                var message = $"{nameof(instanceType)} has not contract : {nameof(contractType)}";
                Assert.Fail(message);
            }

            public static void BindingInterfaceException(Type instanceType, Type contractType)
            {
                var message = $"Cant find {contractType} while building {instanceType}";
                Assert.Fail(message);
            }
        }

        public static class ResolveException
        {
            public static void DoesntExistException(Type type)
            {
                var message = $"{type} Doesnt exist";
                Assert.Fail(message);
            }
        }

        public static class ConstructorException
        {
            public static void ConstructorIsNullException(Type type)
            {
                var message = $"{type} Constructor is null";
                Assert.Fail(message);
            }

            public static void IsWrongParamsCountException(Type type, int requiredParametersCount)
            {
                var message = $"{type} Constructor params not equals required parameters - {requiredParametersCount}";
                Assert.Fail(message);
            }
        }
    }
}