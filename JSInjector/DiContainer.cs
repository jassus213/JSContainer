using System;
using System.Collections.Generic;
using System.Linq;
using JSInjector.Binding;
using JSInjector.Binding.BindInfo;
using JSInjector.Common.Enums;
using JSInjector.Common.TypeInstancePair;
using JSInjector.Contracts;
using JSInjector.Factories;
using JSInjector.JSExceptions;
using JSInjector.Service;

namespace JSInjector
{
    public class DiContainer : IContainer
    {
        internal Dictionary<Type, KeyValuePair<bool, TypeInstancePair>> ContainerInfo =
            new Dictionary<Type, KeyValuePair<bool, TypeInstancePair>>();

        internal Dictionary<Type, IEnumerable<Type>> ContractsInfo = new Dictionary<Type, IEnumerable<Type>>();

        internal Dictionary<Type, BindInformation> BindInfoMap = new Dictionary<Type, BindInformation>();

        internal Dictionary<Type, Dictionary<Type, object>> ScopedInstance =
            new Dictionary<Type, Dictionary<Type, object>>();

        internal readonly Dictionary<Type, FactoryBindInfo>
            FactoryBindInfoMap = new Dictionary<Type, FactoryBindInfo>();

        internal readonly Queue<KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>> BindQueue =
            new Queue<KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>>();

        private readonly IReadOnlyDictionary<InstanceType, Func<Type, BindInformation, object>> _instanceFactoryMap;

        public DiContainer()
        {
            _instanceFactoryMap = new Dictionary<InstanceType, Func<Type, BindInformation, object>>()
            {
                [InstanceType.Default] = (type, bindInfo) =>
                    InstanceFactoryService.FindAndInvokeMethod(this, type, bindInfo),
                [InstanceType.Factory] = (type, bindInfo) =>
                    InstanceFactoryService.FindAndInvokeMethod(this, type, bindInfo),
            };
        }


        public void Initialize()
        {
            foreach (var keyValuePair in BindQueue)
            {
                if (ContainerInfo[keyValuePair.Key].Key)
                    return;
                
                var method = _instanceFactoryMap[BindInfoMap[keyValuePair.Key].InstanceType];
                var instance = method.Invoke(keyValuePair.Key, BindInfoMap[keyValuePair.Key]);
            }

            BindQueue.Clear();
        }

        public TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            var containerInfo = ContainerInfo[type];
            if (containerInfo.Key)
                return (TContract)containerInfo.Value.Instance;

            throw JsExceptions.ResolveException.DoesntExistException(type);
        }

        private ConcreteIdBinder<TContract> Bind<TContract>(BindInformation bindInformation)
        {
            if (!typeof(TContract).IsInterface)
                this.InitializeBindInfo(typeof(TContract), bindInformation, new KeyValuePair<bool, TypeInstancePair>(false, TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return new ConcreteIdBinder<TContract>(this, bindInformation);
        }

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindType.Default, InstanceType.Default, LifeCycle.Default);
            return Bind<TContract>(bindInfo);
        }

        public ConcreteIdBinder<TContract> BindInterfacesTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindType.InterfacesTo, InstanceType.Default, LifeCycle.Default);
            Bind<TContract>(bindInfo);
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public ConcreteIdBinder<TContract> BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindType.SelfTo, InstanceType.Default, LifeCycle.Default);
            Bind<TContract>(bindInfo);
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public ConcreteIdBinder<TContract> BindInterfacesAndSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindType.InterfacesAndSelfTo, InstanceType.Default,
                LifeCycle.Default);
            Bind<TContract>(bindInfo);
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindType.InterfacesAndSelfTo, InstanceType.Factory,
                LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, false, 2);
            this.InitializeBindInfo(factoryType, bindInfo, new KeyValuePair<bool, TypeInstancePair>(false, TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory
            where TArgs : struct
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindType.InterfacesAndSelfTo, InstanceType.Factory,
                LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, true, 3, typeof(TArgs));
            this.InitializeBindInfo(factoryType, bindInfo, new KeyValuePair<bool, TypeInstancePair>(false, TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        private FactoryConcreteBinderId<TFactory> BindFactory<TFactory>(FactoryBindInfo factoryBindInfo)
        {
            this.InitializeFactoryInfoMap(factoryBindInfo.FactoryType, factoryBindInfo);
            return new FactoryConcreteBinderId<TFactory>(this, factoryBindInfo);
        }

        public object InitializeWithOutOrder<TConcrete>()
        {
            var instance = InstanceFactoryService.FindAndInvokeMethod(this, typeof(TConcrete),
                BindInfoMap[typeof(TConcrete)]);
            var pair = new KeyValuePair<bool, TypeInstancePair>(true, TypeInstancePairFactory.CreatePair(instance));
            this.ReWriteInstanceInfo(typeof(TConcrete), BindInfoMap[typeof(TConcrete)], pair);
            return instance;
        }
    }
}