using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using JSInjector.Binding;
using JSInjector.Binding.BindInfo;
using JSInjector.Common;
using JSInjector.Common.Enums;
using JSInjector.Common.Tree;
using JSInjector.Common.TypeInstancePair;
using JSInjector.Contracts;
using JSInjector.Factories;
using JSInjector.JSExceptions;
using JSInjector.Services;
using JSInjector.Utils.Instance;

namespace JSInjector
{
    public class DiContainer : IContainer
    {
        internal Dictionary<Type, KeyValuePair<bool, TypeInstancePair>> ContainerInfo =
            new Dictionary<Type, KeyValuePair<bool, TypeInstancePair>>();

        internal Dictionary<Type, IEnumerable<Type>> ContractsInfo = new Dictionary<Type, IEnumerable<Type>>();

        internal readonly Dictionary<IEnumerable<Type>, ScopeTree> ScopedTree =
            new Dictionary<IEnumerable<Type>, ScopeTree>();

        internal Dictionary<Type, BindInformation> BindInfoMap = new Dictionary<Type, BindInformation>();

        internal readonly Dictionary<Type, FactoryBindInfo>
            FactoryBindInfoMap = new Dictionary<Type, FactoryBindInfo>();

        internal readonly LinkedList<KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>> BindQueue =
            new LinkedList<KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>>();

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
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            SortQueue();

            foreach (var keyValuePair in BindQueue)
            {
                if (!ContainerInfo[keyValuePair.Key].Key)
                {
                    var method = _instanceFactoryMap[BindInfoMap[keyValuePair.Key].InstanceType];
                    var instance = method.Invoke(keyValuePair.Key, BindInfoMap[keyValuePair.Key]);
                }
            }

            BindQueue.Clear();
            stopwatch.Stop();
            var test = stopwatch.Elapsed.TotalSeconds.ToString();
        }

        private void SortQueue()
        {
            foreach (var instanceInfo in ContainerInfo)
            {
                if (!instanceInfo.Value.Key)
                {
                    var isExist = BindInfoMap.Values.ToArray()
                        .Select(x => x.ParameterExpressions.ContainsKey(instanceInfo.Key));

                    if (!isExist.Contains(true))
                        BindQueue.AddFirst(
                            new KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>(instanceInfo.Key,
                                instanceInfo.Value));
                    else
                        BindQueue.AddLast(
                            new KeyValuePair<Type, KeyValuePair<bool, TypeInstancePair>>(instanceInfo.Key,
                                instanceInfo.Value));
                }
            }
        }

        public TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            var containerInfo = ContainerInfo[type];
            if (containerInfo.Key)
                return (TContract)containerInfo.Value.Instance;

            throw JsExceptions.ResolveException.DoesntExistException(type);
        }

        public IReadOnlyCollection<TContract> ResolveAll<TContract>()
        {
            var instancesTypes = ContractsInfo[typeof(TContract)].ToList();
            var instances = instancesTypes
                .Select(x => (TContract)ContainerInfo[x].Value.Instance).ToArray();
            return instances;
        }

        private ConcreteIdBinder<TContract> Bind<TContract>(BindInformation bindInformation)
        {
            if (!typeof(TContract).IsInterface)
                this.InitializeBindInfo(typeof(TContract), bindInformation,
                    new KeyValuePair<bool, TypeInstancePair>(false,
                        TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return ConcreteBindersFactory.Create<TContract>(this, bindInformation);
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
            var bindInformation = this.GetBindInfo(type, BindType.InterfacesTo, InstanceType.Default, LifeCycle.Default);
            return Bind<TContract>(bindInformation);
        }

        public ConcreteIdBinder<TContract> BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInformation = this.GetBindInfo(type, BindType.SelfTo, InstanceType.Default, LifeCycle.Default);
            return Bind<TContract>(bindInformation);
        }

        public ConcreteIdBinder<TContract> BindInterfacesAndSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInformation = this.GetBindInfo(type, BindType.InterfacesAndSelfTo, InstanceType.Default,
                LifeCycle.Default);
            return Bind<TContract>(bindInformation);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindType.InterfacesAndSelfTo, InstanceType.Factory,
                LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, false, 2);
            this.InitializeBindInfo(factoryType, bindInfo,
                new KeyValuePair<bool, TypeInstancePair>(false,
                    TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
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
            this.InitializeBindInfo(factoryType, bindInfo,
                new KeyValuePair<bool, TypeInstancePair>(false,
                    TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
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