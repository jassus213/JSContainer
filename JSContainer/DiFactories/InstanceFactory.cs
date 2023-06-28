using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using JSContainer.Common.TypeInstancePair;
using JSContainer.Utils.Instance;

namespace JSContainer.DiFactories
{
    internal static class InstanceFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TConcrete>(ConstructorInfo constructorInfo, DiContainer diContainer)
        {
            var func = FunctionFactory.CreateFunc<TConcrete>(constructorInfo);
            var instance = func.Invoke();
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer) where TConcrete : class where TArg1 : class
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FunctionFactory.CreateFunc<TArg1, TConcrete>(constructorInfo, parameters);
            var instance = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1, TConcrete>(diContainer));
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer) where TConcrete : class where TArg1 : class where TArg2 : class
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FunctionFactory.CreateFunc<TArg1, TArg2, TConcrete>(constructorInfo, parameters);
            var instance = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1, TConcrete>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2, TConcrete>(diContainer));
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer) where TConcrete : class where TArg1 : class where TArg2 : class where TArg3 : class
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FunctionFactory.CreateFunc<TArg1, TArg2, TArg3, TConcrete>(constructorInfo, parameters);
            var instance = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1, TConcrete>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2, TConcrete>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3, TConcrete>(diContainer));
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer) where TConcrete : class
            where TArg2 : class
            where TArg1 : class
            where TArg3 : class
            where TArg4 : class
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FunctionFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TConcrete>(constructorInfo, parameters);
            var instance = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1, TConcrete>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2, TConcrete>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3, TConcrete>(diContainer),
                (TArg4)DiContainerManager.SearchInstance<TArg4, TConcrete>(diContainer));
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));
            return instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(
            ConstructorInfo constructorInfo,
            DiContainer diContainer) where TConcrete : class
            where TArg1 : class
            where TArg2 : class
            where TArg3 : class
            where TArg4 : class
            where TArg5 : class
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FunctionFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(constructorInfo,
                parameters);
            var instance = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1, TConcrete>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2, TConcrete>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3, TConcrete>(diContainer),
                (TArg4)DiContainerManager.SearchInstance<TArg4, TConcrete>(diContainer),
                (TArg5)DiContainerManager.SearchInstance<TArg5, TConcrete>(diContainer));
            diContainer.ReWriteInstanceInfo(typeof(TConcrete),
                diContainer.BindInfoMap[typeof(TConcrete)],
                (true, TypeInstancePairFactory.CreatePair(instance)));

            return instance;
        }
    }
}