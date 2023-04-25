using System.Collections.Generic;
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
            var obj = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1>(diContainer));
            diContainer.ReWriteInstanceInfo(obj.GetType(), diContainer.BindInfoMap[obj.GetType()],
                new KeyValuePair<bool, object>(true, obj));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2>(diContainer));
            diContainer.ReWriteInstanceInfo(obj.GetType(), diContainer.BindInfoMap[obj.GetType()],
                new KeyValuePair<bool, object>(true, obj));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3>(diContainer));
            diContainer.ReWriteInstanceInfo(obj.GetType(), diContainer.BindInfoMap[obj.GetType()],
                new KeyValuePair<bool, object>(true, obj));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TConcrete>(ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TConcrete>(constructorInfo, parameters);
            var obj = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3>(diContainer),
                (TArg4)DiContainerManager.SearchInstance<TArg4>(diContainer));
            diContainer.ReWriteInstanceInfo(obj.GetType(), diContainer.BindInfoMap[obj.GetType()],
                new KeyValuePair<bool, object>(true, obj));
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TConcrete CreateInstance<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(
            ConstructorInfo constructorInfo,
            DiContainer diContainer)
        {
            var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(typeof(TConcrete));
            var func = FuncFactory.CreateFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>(constructorInfo,
                parameters);
            var obj = func.Invoke((TArg1)DiContainerManager.SearchInstance<TArg1>(diContainer),
                (TArg2)DiContainerManager.SearchInstance<TArg2>(diContainer),
                (TArg3)DiContainerManager.SearchInstance<TArg3>(diContainer),
                (TArg4)DiContainerManager.SearchInstance<TArg4>(diContainer),
                (TArg5)DiContainerManager.SearchInstance<TArg5>(diContainer));

            return obj;
        }

        /*private static object IsInstanced<TConcrete>(DiContainer diContainer)
        {
            if (diContainer.ContainerInfo[typeof(TConcrete)].Key)
                return diContainer.ContainerInfo[typeof(TConcrete)].Value;

            return null;
        }*/
    }
}