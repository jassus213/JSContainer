using System;
using System.Collections.Generic;
using System.Reflection;
using JSInjector.Binding;
using TestProject;

namespace JSInjector
{
    public class DiContainer
    {
        /*public readonly Dictionary<ConcreteIdBinder<>, Type[]> _container = new Dictionary<ConcreteIdBinder, Type[]>();*/
        public readonly Dictionary<Type, BindInfo> BindInfoMap = new Dictionary<Type, BindInfo>();
        public readonly Type[] TypesList;
        private readonly List<Type> _types = new List<Type>();
        private readonly List<Type> _contractTypes = new List<Type>();

        public DiContainer()
        {
            TypesList = _types.ToArray();
        }

        public void Instantiate<TContract>()
        {
            var type = typeof(TContract);
            var currentObjInfo = BindInfoMap[type];
            var contractsTypes = currentObjInfo.TypesMap[currentObjInfo.CurrentType].ToArray();
            var objParams = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                CallingConventions.HasThis, contractsTypes, null)?.GetParameters();
            List<object> parameters = new List<object>();

            foreach (var param in objParams)
            {
                var typeOfParam = param.ParameterType;
                parameters.Add(Activator.CreateInstance(typeOfParam));
            }

            var item = Activator.CreateInstance(type, BindingFlags.Instance, parameters);
            Activator.CreateInstance(type, objParams);
            var manager = (Manager)item;
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