using System;

namespace JSInjector.JSExceptions
{
    public static class JsException
    {
        public static readonly Func<string, Exception> Exception = (message) => new Exception(message);
    }

    public static class JsExceptions
    {
        public static class BindException
        {
            public static Exception NotBindedException(Type type)
            {
                var message = $"{type} Is not binded";
                return JsException.Exception(message);
            }

            public static Exception AlreadyBindedException(Type type)
            {
                var message = $"{type} Already Binded";
                return JsException.Exception(message);
            }

            public static Exception CircularDependency(Type instanceType,
                Type parameterType)
            {
                var message = $"Circular Dependency {instanceType} and {parameterType}";
                return JsException.Exception(message);
            }

            public static Exception ContractNotBindedToInstance(Type instanceType, Type contractType)
            {
                var message = $"{instanceType} has not contract : {contractType}";
                return JsException.Exception(message);
            }

            public static Exception BindingInterfaceException(Type instanceType, Type contractType)
            {
                var message = $"Cant find {contractType} while building {instanceType}";
                return JsException.Exception(message);
            }
        }

        public static class ResolveException
        {
            public static Exception DoesntExistException(Type type)
            {
                var message = $"{type} Doesnt exist";
                return JsException.Exception(message);
            }
        }

        public static class ConstructorException
        {
            public static Exception ConstructorIsNullException(Type type)
            {
                var message = $"{type} Constructor is null";
                return JsException.Exception(message);
            }

            public static Exception IsWrongParamsCountException(Type type, int requiredParametersCount)
            {
                var message = $"{type} Constructor params not equals required parameters - {requiredParametersCount}";
                return JsException.Exception(message);
            }
        }
    }
}