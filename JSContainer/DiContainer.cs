using System;
using System.Collections.Generic;
using System.Linq;
using JSContainer.Binding;
using JSContainer.Binding.BindInfo;
using JSContainer.Common.Enums;
using JSContainer.Common.Tree;
using JSContainer.Common.TypeInstancePair;
using JSContainer.Contracts;
using JSContainer.JSExceptions;
using JSContainer.Services;

namespace JSContainer
{
    public class DiContainer : IContainer 
    {
        internal Dictionary<Type, (bool IsCreated, TypeInstancePair TypeInstancePair)> ContainerInfo =
            new Dictionary<Type, (bool, TypeInstancePair)>();

        internal Dictionary<Type, IEnumerable<Type>> ContractsInfo = new Dictionary<Type, IEnumerable<Type>>();

        internal readonly Dictionary<IEnumerable<Type>, ScopeTree> ScopedTree =
            new Dictionary<IEnumerable<Type>, ScopeTree>();

        internal Dictionary<Type, BindInformation> BindInfoMap = new Dictionary<Type, BindInformation>();

        internal readonly LinkedList<KeyValuePair<Type, (bool IsCreated, TypeInstancePair TypeInstancePair)>>
            BindQueue =
                new LinkedList<KeyValuePair<Type, (bool, TypeInstancePair)>>();


        public void Initialize()
        {
            SortQueue();

            foreach (var keyValuePair in BindQueue)
            {
                if (!ContainerInfo[keyValuePair.Key].IsCreated)
                {
                    var type = keyValuePair.Value.TypeInstancePair.Type;
                    var method = InstanceFactoryService.FindAndInvokeMethod(this, type, BindInfoMap[type]);
                }
            }

            BindQueue.Clear();
        }


        private void SortQueue()
        {
            foreach (var instanceInfo in ContainerInfo)
            {
                if (!instanceInfo.Value.IsCreated)
                {
                    var isExist = BindInfoMap.Values.ToArray()
                        .Select(x => x.Parameters.ContainsKey(instanceInfo.Key));

                    var tuple = new KeyValuePair<Type, (bool IsCreated, TypeInstancePair TypeInstancePair)>(
                        instanceInfo.Key, instanceInfo.Value);

                    if (!isExist.Contains(true))
                        BindQueue.AddFirst(tuple);
                    else
                        BindQueue.AddLast(tuple);
                }
            }
        }

        public TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            var containerInfo = ContainerInfo[type];
            if (containerInfo.IsCreated)
                return (TContract)containerInfo.TypeInstancePair.Instance;

            throw JsExceptions.ResolveException.DoesntExistException(type);
        }

        public IReadOnlyCollection<TContract> ResolveAll<TContract>()
        {
            var instancesTypes = ContractsInfo[typeof(TContract)].ToList();
            var instances = instancesTypes
                .Select(x => (TContract)ContainerInfo[x].TypeInstancePair.Instance).ToArray();
            return instances;
        }

        private ConcreteIdBinder<TContract> Bind<TContract>(BindInformation bindInformation)
        {
            if (!typeof(TContract).IsInterface)
                this.InitializeBindInfo(typeof(TContract), bindInformation,
                    (false, TypeInstancePairFactory.CreatePairWithCurrentType(typeof(TContract), null)));
            return ConcreteBindersFactory.Create<TContract>(this, bindInformation);
        }

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindType.Default, LifeTime.Default);
            return Bind<TContract>(bindInfo);
        }

        public ConcreteIdBinder<TContract> BindInterfacesTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInformation =
                this.GetBindInfo(type, BindType.InterfacesTo, LifeTime.Default);
            return Bind<TContract>(bindInformation);
        }

        public ConcreteIdBinder<TContract> BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInformation = this.GetBindInfo(type, BindType.SelfTo, LifeTime.Default);
            return Bind<TContract>(bindInformation);
        }

        public ConcreteIdBinder<TContract> BindInterfacesAndSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInformation = this.GetBindInfo(type, BindType.InterfacesAndSelfTo, LifeTime.Default);
            return Bind<TContract>(bindInformation);
        }

        /*public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindType.InterfacesAndSelfTo, LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, false, 2);
            this.InitializeBindInfo(factoryType, bindInfo,
                (false, TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory
            where TArgs : struct
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindType.InterfacesAndSelfTo, LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, true, 3, typeof(TArgs));
            this.InitializeBindInfo(factoryType, bindInfo,
                (false, TypeInstancePairFactory.CreatePairWithCurrentType(null, null)));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        private FactoryConcreteBinderId<TFactory> BindFactory<TFactory>(FactoryBindInfo factoryBindInfo)
        {
            this.InitializeFactoryInfoMap(factoryBindInfo.FactoryType, factoryBindInfo);
            return new FactoryConcreteBinderId<TFactory>(this, factoryBindInfo);
        }
        */

        public object InitializeWithOutOrder<TConcrete>()
        {
            var instance = InstanceFactoryService.FindAndInvokeMethod(this, typeof(TConcrete),
                BindInfoMap[typeof(TConcrete)]);
            var tuple =(true, TypeInstancePairFactory.CreatePair(instance));
            this.ReWriteInstanceInfo(typeof(TConcrete), BindInfoMap[typeof(TConcrete)], tuple);
            return instance;
        }
    }
}