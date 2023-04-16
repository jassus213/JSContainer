using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JSInjector.Binding;
using NUnit.Framework;

namespace JSInjector
{
    public class DiContainer
    {
        internal readonly Dictionary<Type, KeyValuePair<bool, object>> ContainerInfo = new Dictionary<Type, KeyValuePair<bool, object>>();
        internal readonly Dictionary<Type, BindInfo> BindInfoMap = new Dictionary<Type, BindInfo>();
        internal readonly List<Type> AllowedTypes = new List<Type>();

        private readonly ObjCreator _objCreator;

        public DiContainer()
        {
            _objCreator = new ObjCreator(this);
        }

        public void Instantiate(DiContainer container)
        {
            Type type = null;
            ParameterInfo[] parameterInfos = new ParameterInfo[] { };
            
            
            foreach (var keyValuePair in container.BindInfoMap)
            {
                if (!AllowedTypes.Contains(keyValuePair.Key))
                {
                    Assert.Fail( keyValuePair.Key + "Not binded");
                    return;
                }
                
                type = keyValuePair.Key;
                var contractTypes = keyValuePair.Value.TypesMap[type].ToArray();
                parameterInfos = _objCreator.GetParamsInfo(type, contractTypes, CallingConventions.HasThis);
                var obj = _objCreator.TryCreateObj(type, parameterInfos);
            }
        }

        

        

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = GetBindInfo(type);
            InitializeInfo(type, bindInfo, new KeyValuePair<bool, object>(false, null));
            return new ConcreteIdBinder<TContract>(this, bindInfo, _objCreator);
        }

        public TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            var containerInfo = ContainerInfo[type];
            if (containerInfo.Key)
                return (TContract)containerInfo.Value;
            
            Assert.Fail(type + "Doesnt exist");
            return (TContract)containerInfo.Value;        

        }

        public void BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = GetBindInfo(type);
            var parameters = _objCreator.GetParamsInfo(type, AllowedTypes.ToArray(), CallingConventions.Any);
            InitializeInfo(type, bindInfo, new KeyValuePair<bool, object>(false, null));
            var obj = _objCreator.TryCreateObj<TContract>();
        }

        private void InitializeInfo(Type type, BindInfo bindInfo, KeyValuePair<bool, object> keyValuePair)
        {
            if (!ContainerInfo.ContainsKey(type))
            {
                var tupleObj = keyValuePair;
                ContainerInfo.Add(type, tupleObj);
                AllowedTypes.Add(type);
                BindInfoMap.Add(type, bindInfo);
            }
        }

        private BindInfo GetBindInfo(Type type)
        {
            if (BindInfoMap.ContainsKey(type))
                return BindInfoMap[type];

            return new BindInfo(type);
        }

        public BindInfo GetBindInfo<T>()
        {
            var currentObj = BindInfoMap[typeof(T)];
            return currentObj;
        }
    }
}