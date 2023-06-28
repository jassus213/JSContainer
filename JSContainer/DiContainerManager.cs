using System.Linq;
using JSContainer.Binding.BindInfo;
using JSContainer.Common.Enums;
using JSContainer.Common.Tree;
using JSContainer.JSExceptions;
using JSContainer.Services;
using JSContainer.Utils.Instance;
using JSContainer.Utils.LifeCycle;

namespace JSContainer
{
    internal static class DiContainerManager
    {
        internal static object SearchInstance<TInstance, TConcrete>(DiContainer container)
            where TConcrete : class where TInstance : class
        {
            var type = typeof(TInstance);
            var typeConcrete = typeof(TConcrete);
            var currentType = type;
            BindInformation bindInformation = null;


            if (InstanceUtil.IsInterfaceAndBinded(currentType, ref container.ContractsInfo, ref container.BindInfoMap))
            {
                currentType = container.ContractsInfo[type].Last();

                bindInformation = container.BindInfoMap[currentType];

                if (!bindInformation.ContractsTypes.Contains(type))
                    throw JsExceptions.BindException.ContractNotBindedToInstance(type, currentType);
            }
            else if (container.BindInfoMap.ContainsKey(currentType))
            {
                bindInformation = container.BindInfoMap[currentType];
            }

            TreeManager.InitializeTree(container, typeConcrete);


            if (container.BindInfoMap[typeConcrete].ArgumentsMap
                .ContainsKey(currentType)) // Get argument from TConcrete
            {
                bindInformation = container.BindInfoMap[typeConcrete];
                var instance = bindInformation.ArgumentsMap[currentType];
                return instance;
            }


            if (InstanceUtil.ParametersUtil.HasCircularDependency(container, type,
                    container.BindInfoMap[currentType].Parameters.Keys.ToList()))
                return null;


            if (bindInformation!.LifeTime == LifeTime.Singleton)
            {
                if (!container.IsSingletonInstanced(currentType))
                {
                    var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                    var instance = genericMethod.Invoke(null,
                        new object[]
                        {
                            InstanceUtil.ConstructorUtils.GetConstructor(currentType,
                                ConstructorConventionsSequence.First),
                            container
                        });
                    
                    return instance;
                }

                return container.ContainerInfo[currentType].TypeInstancePair.Instance;
            }


            if (bindInformation!.LifeTime == LifeTime.Scoped)
            {
                var keyValuePair = container.ScopedTree.First(x => x.Key.Contains(typeConcrete));
                var scopeTree = keyValuePair.Value;

                if (scopeTree.ScopeInstance != null)
                {
                    return scopeTree.ScopeInstance;
                }

                var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                var instance = genericMethod.Invoke(null,
                    new object[]
                    {
                        InstanceUtil.ConstructorUtils.GetConstructor(currentType, ConstructorConventionsSequence.First),
                        container
                    });

                container.ScopedTree[keyValuePair.Key].InitializeObject(instance);

                return instance;
            }


            if (bindInformation!.LifeTime == LifeTime.Transient)
            {
                var genericMethod = InstanceFactoryService.FindMethod(container, currentType);
                var obj = genericMethod.Invoke(null,
                    new object[]
                    {
                        InstanceUtil.ConstructorUtils.GetConstructor(currentType, ConstructorConventionsSequence.First),
                        container
                    });
                return obj;
            }

            return null;
        }
    }
}