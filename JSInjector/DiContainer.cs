using System;
using System.Collections.Generic;
using System.Linq;
using JSInjector.Binding;
using JSInjector.Binding.BindInfo;
using JSInjector.Factories;
using JSInjector.JSExceptions;

namespace JSInjector
{
    public class DiContainer
    {
        internal readonly Dictionary<Type, KeyValuePair<bool, object>> ContainerInfo =
            new Dictionary<Type, KeyValuePair<bool, object>>();

        internal readonly Dictionary<Type, BindInfo> BindInfoMap = new Dictionary<Type, BindInfo>();
        internal readonly Dictionary<Type, FactoryBindInfo> FactoryBindInfoMap = new Dictionary<Type, FactoryBindInfo>();
        private readonly Queue<KeyValuePair<Type, KeyValuePair<bool, object>>> _queue = new Queue<KeyValuePair<Type, KeyValuePair<bool, object>>>();

        private readonly ObjectInitializer _objectInitializer;

        public DiContainer()
        {
            _objectInitializer = new ObjectInitializer(this);
        }


        public void Initialize()
        {
            foreach (var keyValuePair in _queue)
            {
                if (!keyValuePair.Value.Key)
                {
                    switch (BindInfoMap[keyValuePair.Key].InstanceType)
                    {
                        case InstanceType.Default:
                            var baseMethod = typeof(ObjectInitializer).GetMethod("CreateInstance");
                            var genericMethod = baseMethod.MakeGenericMethod(keyValuePair.Key);
                            genericMethod.Invoke(_objectInitializer, null);
                            break;
                        case InstanceType.Factory:
                            var baseMethodFactory = typeof(FactoryInitializer).GetMethods().Where(x => x.Name == "Create" && x.GetGenericArguments().Length == FactoryBindInfoMap[keyValuePair.Key].GenericArguments).ToArray().First();
                            break;
                    }
                }
            }
            
            _queue.Clear();
        }
        
        public TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            var containerInfo = ContainerInfo[type];
            if (containerInfo.Key)
                return (TContract)containerInfo.Value;

            JsExceptions.ResolveException.DoesntExistException(type);
            return (TContract)containerInfo.Value;
        }

        private ConcreteIdBinder<TContract> Bind<TContract>(BindInfo bindInfo)
        {
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = GetBindInfo(type, BindTypes.Default, InstanceType.Default);
            return Bind<TContract>(bindInfo);
        }

        public ConcreteIdBinder<TContract> BindInterfacesTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = GetBindInfo(type, BindTypes.InterfacesTo, InstanceType.Default);
            var interfaces = type.GetInterfaces();
            bindInfo.ContractsTypes.AddRange(interfaces);
            return Bind<TContract>(bindInfo);
        }

        public void BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = GetBindInfo(type, BindTypes.SelfTo, InstanceType.Default);
            InitializeInfo(type, bindInfo, new KeyValuePair<bool, object>(false, null));
            //var obj = _objectInitializer.CreateInstance<TContract>();
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>()
            where TFactory : IFactory
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = GetBindInfo(factoryType, BindTypes.Default, InstanceType.Factory);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, false, 2);
            InitializeInfo(factoryType, bindInfo, new KeyValuePair<bool, object>(false, null));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory
            where TArgs : struct
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = GetBindInfo(factoryType, BindTypes.Default, InstanceType.Factory);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, true, 3,typeof(TArgs));
            InitializeInfo(factoryType, bindInfo, new KeyValuePair<bool, object>(false, null));
            return BindFactory<TFactory>(factoryBindInfo);
        }
        
        private FactoryConcreteBinderId<TFactory> BindFactory<TFactory>(FactoryBindInfo factoryBindInfo)
        {
            InitializeFactoryInfoMap(factoryBindInfo.FactoryType, factoryBindInfo);
            return new FactoryConcreteBinderId<TFactory>(this, factoryBindInfo);
        }

        private void InitializeInfo(Type type, BindInfo bindInfo, KeyValuePair<bool, object> keyValuePair)
        {
            if (!ContainerInfo.ContainsKey(type))
            {
                _queue.Enqueue(new KeyValuePair<Type, KeyValuePair<bool, object>>(type, keyValuePair));
                ContainerInfo.Add(type, keyValuePair);
                BindInfoMap.Add(type, bindInfo);
            }
        }

        internal void InitializeFromResolve(Type type, BindTypes bindTypes, KeyValuePair<bool, object> keyValuePair)
        {
            if (!ContainerInfo.ContainsKey(type))
            {
                var bindInfo = new BindInfo(type, bindTypes, InstanceType.Default);
                ContainerInfo.Add(type, keyValuePair);
                BindInfoMap.Add(type, bindInfo);
            }
        }

        private void InitializeFactoryInfoMap(Type type, FactoryBindInfo factoryBindInfo)
        {
            FactoryBindInfoMap.Add(type, factoryBindInfo);
        }

        internal void ReWriteContainerInfo(Type type, KeyValuePair<bool, object> keyValuePair)
        {
            if (!ContainerInfo.ContainsKey(type))
            {
                JsExceptions.BindException.NotBindedException(type);
                return;
            }

            ContainerInfo.Remove(type);
            ContainerInfo.Add(type, keyValuePair);
        }

        private BindInfo GetBindInfo(Type type, BindTypes bindType, InstanceType instanceType)
        {
            if (BindInfoMap.ContainsKey(type))
                return BindInfoMap[type];

            return new BindInfo(type, bindType, instanceType);
        }
    }
}