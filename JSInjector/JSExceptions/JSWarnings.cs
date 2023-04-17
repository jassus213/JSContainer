using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace JSInjector.JSExceptions
{
    internal static class JsWarnings
    {
        internal static class ConstructorWarnings
        {
            internal static void LotConstructorReturnedWarning(int countOfConstructors, ConstructorInfo[] constructorInfos)
            {
                var message = $"A lot of constructors were returned, namely {countOfConstructors}, the first one was taken {constructorInfos.First()}";
                Assert.Warn(message);
                
                foreach (var constructor in constructorInfos)
                {
                    var msg = $"One of the constructors : {constructor}";
                    Assert.Warn(msg);
                }
            }
        }
    }
}