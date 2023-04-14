using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JSInjector.Binding;
using NUnit.Framework;
using TestProject;

namespace JSInjector
{
    public class DiContainer
    {
        private readonly Dictionary<Type, object> _container = new Dictionary<Type, object>();
        public readonly Dictionary<Type, BindInfo> BindInfoMap = new Dictionary<Type, BindInfo>();
        public readonly Type[] TypesList;
        private readonly List<Type> _types = new List<Type>();
        private readonly List<Type> _contractTypes = new List<Type>();

        public DiContainer()
        {
            TypesList = _types.ToArray();
        }

        public void Instantiate(DiContainer container)
        {
            Type type = null;
            ParameterInfo[] parameterInfos = new ParameterInfo[] { };
            
            foreach (var keyValuePair in container.BindInfoMap)
            {
                if (!_types.Contains(keyValuePair.Key))
                {
                    Assert.Fail( keyValuePair.Key + "Not binded");
                    return;
                }
                
                type = keyValuePair.Key;
                var contractTypes = keyValuePair.Value.TypesMap[type].ToArray();
                parameterInfos = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                    CallingConventions.HasThis, contractTypes, null)?.GetParameters();
                var obj = CreateObj(type, parameterInfos);
                _container.Add(type, obj);
            }

        }

        private Object CreateObj(Type type, ParameterInfo[] parameterInfos)
        {
            List<object> parameters = new List<object>();
            

            try
            {
                foreach (var param in parameterInfos)
                {
                    var typeOfParam = param.ParameterType;
                    parameters.Add(Activator.CreateInstance(typeOfParam));
                }
                
                return Activator.CreateInstance(type, parameters.ToArray());
            }
            catch (Exception e)
            {
                Assert.Fail("Error while building " + type);
            }

            return null;
        }

        public bool CanBind<T>()
        {
            if (typeof(T).GetConstructors().Length == 0)
                return false;
            return true;
        }

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = new BindInfo();
            _types.Add(type);
            bindInfo.TypesMap.Add(type, new List<Type>());
            bindInfo.CurrentType = type;
            BindInfoMap.Add(type, bindInfo);
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public BindInfo GetBindInfo<T>()
        {
            var currentObj = BindInfoMap[typeof(T)];
            return currentObj;
        }

        public TConcrete To<TConcrete>() where TConcrete : new()
        {
            _types.Add(typeof(TConcrete));
            return new TConcrete();
        }
    }
}