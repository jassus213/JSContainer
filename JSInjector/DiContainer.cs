﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JSInjector.Binding;
using JSInjector.Binding.BindInfo;
using JSInjector.Contracts;
using JSInjector.DiFactories;
using JSInjector.Factories;
using JSInjector.JSExceptions;
using JSInjector.Utils;

namespace JSInjector
{
    public class DiContainer : IContainer
    {
        internal readonly Dictionary<Type, KeyValuePair<bool, object>> ContainerInfo =
            new Dictionary<Type, KeyValuePair<bool, object>>();

        internal readonly Dictionary<Type, IEnumerable<Type>> ContractsInfo = new Dictionary<Type, IEnumerable<Type>>();

        internal readonly Dictionary<Type, BindInfo> BindInfoMap = new Dictionary<Type, BindInfo>();

        internal readonly Dictionary<Type, FactoryBindInfo>
            FactoryBindInfoMap = new Dictionary<Type, FactoryBindInfo>();

        internal readonly Queue<KeyValuePair<Type, KeyValuePair<bool, object>>> BindQueue =
            new Queue<KeyValuePair<Type, KeyValuePair<bool, object>>>();


        public void Initialize()
        {
            foreach (var keyValuePair in BindQueue.Where(keyValuePair => !keyValuePair.Value.Key))
            {
                switch (BindInfoMap[keyValuePair.Key].InstanceType)
                {
                    case InstanceType.Default:
                        var baseMethods =
                            typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
                        var currentMethod = baseMethods.First(x =>
                            x.GetGenericArguments().Length - 1 ==
                            BindInfoMap[keyValuePair.Key].ParameterExpressions.Count && x.Name == "CreateInstance");
                        var genericMethod = currentMethod.MakeGenericMethod(InstanceUtil.GenericParameters
                            .GenericArgumentsMap(keyValuePair.Key, BindInfoMap[keyValuePair.Key].ParameterExpressions)
                            .ToArray());
                        var obj = genericMethod.Invoke(this,
                            new object[] { InstanceUtil.ConstructorUtils.GetConstructor(keyValuePair.Key), this });
                        break;
                    case InstanceType.Factory:
                        /*var baseMethodsFactory = typeof(InstanceFactory).GetMethods().First(x => x.Name == "Create" && x.GetGenericArguments().Length - 1 ==
                            FactoryBindInfoMap[keyValuePair.Key].GenericArguments);*/
                        var baseMethodsFactory =
                            typeof(InstanceFactory).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
                        var baseMethod = baseMethodsFactory.First(x =>
                            x.GetGenericArguments().Length - 1 ==
                            BindInfoMap[keyValuePair.Key].ParameterExpressions.Count && x.Name == "CreateInstance");
                        var genericMethodFactory = baseMethod.MakeGenericMethod(InstanceUtil.GenericParameters
                            .GenericArgumentsMap(keyValuePair.Key, BindInfoMap[keyValuePair.Key].ParameterExpressions)
                            .ToArray());
                        var objFactory = genericMethodFactory.Invoke(this,
                            new object[] { InstanceUtil.ConstructorUtils.GetConstructor(keyValuePair.Key), this });
                        break;
                }
            }
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
            this.InitializeBindInfo(typeof(TContract), bindInfo, new KeyValuePair<bool, object>(false, null));
            return new ConcreteIdBinder<TContract>(this, bindInfo);
        }

        public ConcreteIdBinder<TContract> Bind<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindTypes.Default, InstanceType.Default, LifeCycle.Default);
            return Bind<TContract>(bindInfo);
        }

        public void BindInterfacesTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindTypes.InterfacesTo, InstanceType.Default, LifeCycle.Default);
            Bind<TContract>(bindInfo);
        }

        public void BindSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindTypes.SelfTo, InstanceType.Default, LifeCycle.Default);
            this.InitializeBindInfo(type, bindInfo, new KeyValuePair<bool, object>(false, null));
            Bind<TContract>();
        }

        public ConcreteIdLifeCycle<TContract> BindInterfacesAndSelfTo<TContract>()
        {
            var type = typeof(TContract);
            var bindInfo = this.GetBindInfo(type, BindTypes.InterfacesAndSelfTo, InstanceType.Default,
                LifeCycle.Default);
            this.InitializeBindInfo(type, bindInfo, new KeyValuePair<bool, object>(false, null));
            return new ConcreteIdLifeCycle<TContract>(this);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TResult>() where TFactory : IFactory
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindTypes.InterfacesAndSelfTo, InstanceType.Factory,
                LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, false, 2);
            this.InitializeBindInfo(factoryType, bindInfo, new KeyValuePair<bool, object>(false, null));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        public FactoryConcreteBinderId<TFactory> BindFactory<TFactory, TArgs, TResult>() where TFactory : IFactory
            where TArgs : struct
        {
            var factoryType = typeof(TFactory);
            var resultType = typeof(TResult);
            var bindInfo = this.GetBindInfo(factoryType, BindTypes.InterfacesAndSelfTo, InstanceType.Factory,
                LifeCycle.Default);
            var factoryBindInfo = new FactoryBindInfo(factoryType, resultType, true, 3, typeof(TArgs));
            this.InitializeBindInfo(factoryType, bindInfo, new KeyValuePair<bool, object>(false, null));
            return BindFactory<TFactory>(factoryBindInfo);
        }

        private FactoryConcreteBinderId<TFactory> BindFactory<TFactory>(FactoryBindInfo factoryBindInfo)
        {
            this.InitializeFactoryInfoMap(factoryBindInfo.FactoryType, factoryBindInfo);
            return new FactoryConcreteBinderId<TFactory>(this, factoryBindInfo);
        }
    }
}