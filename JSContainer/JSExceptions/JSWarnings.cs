using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace JSContainer.JSExceptions
{
    internal static class JsWarning
    {
        internal static WarningException Warning(string message) => new WarningException(message);
    }
    
    internal static class JsWarnings
    {
        internal static class ConstructorWarnings
        {
            internal static WarningException LotConstructorReturnedWarning(int countOfConstructors, ConstructorInfo[] constructorInfos)
            {
                var message = $"A lot of constructors were returned, namely {countOfConstructors}, the first one was taken {constructorInfos.First()}";
                return JsWarning.Warning(message);
                
                foreach (var constructor in constructorInfos)
                {
                    var msg = $"One of the constructors : {constructor}";
                }
            }
        }
    }
}