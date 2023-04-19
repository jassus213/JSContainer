using System.Reflection;
using System.Runtime.CompilerServices;
using JSInjector.Utils;

namespace JSInjector.DiFactories
{
    internal static class InstanceFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TConcrete>(ConstructorInfo constructorInfo, DiContainer diContainer)
        {
            var func = FuncFactory.CreateFunc<TConcrete>(constructorInfo);
            var obj = func.Invoke();
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerUtil.SearchInstance<TArg2>(diContainer));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerUtil.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerUtil.SearchInstance<TArg3>(diContainer));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerUtil.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerUtil.SearchInstance<TArg3>(diContainer),
                (TArg4)DiContainerUtil.SearchInstance<TArg4>(diContainer));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(
            ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerUtil.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerUtil.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerUtil.SearchInstance<TArg3>(diContainer),
                (TArg4)DiContainerUtil.SearchInstance<TArg4>(diContainer),
                (TArg5)DiContainerUtil.SearchInstance<TArg5>(diContainer));
            return obj;
        }
    }
}