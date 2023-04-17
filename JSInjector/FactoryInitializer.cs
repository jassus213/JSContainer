using System;
using System.Linq;
using System.Linq.Expressions;
using JSInjector.Utils;

namespace JSInjector
{
    internal static class FactoryInitializer
    {
        internal static TInstance Create<TInstance>()
        {
            return DynamicTypeInstanceFactory<TInstance>.Create();
        }

        internal static TInstance Create<TInstance, TArgs>(TArgs args)
        {
            return DynamicTypeInstanceFactory<TInstance, TArgs>.Create(args);
        }

        private static class DynamicTypeInstanceFactory<TInstance>
        {
            public static readonly Func<TInstance> Create = GenerateFactory();

            private static Func<TInstance> GenerateFactory()
            {
                var type = typeof(TInstance);

                var expressions = new Expression[] { Expression.New(type) };
                var expressionBlock = Expression.Block(type, expressions);

                return Expression.Lambda<Func<TInstance>>(expressionBlock).Compile();
            }
        }

        private static class DynamicTypeInstanceFactory<TInstance, TArgs>
        {
            public static readonly Func<TArgs, TInstance> Create = GenerateFactory();

            private static Func<TArgs, TInstance> GenerateFactory()
            {
                var type = typeof(TInstance);
                var typeArg1 = typeof(TArgs);

                var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type);
                if (constructor == null)
                {
                    throw new InvalidOperationException($"Constructor can't be found for type '{type.Name}'");
                }

                var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(new[] { typeArg1 });

                var expression = Expression.New(constructor, parameters);
                var expressionBlock = Expression.Block(type, expression);

                return Expression.Lambda<Func<TArgs, TInstance>>(expressionBlock, parameters).Compile();
            }
        }
        
        private static class DynamicTypeInstanceFactory<TInstance, TArg1, TArg2>
        {
            public static readonly Func<TArg1, TArg2, TInstance> Create = GenerateFactory();

            private static Func<TArg1, TArg2, TInstance> GenerateFactory()
            {
                var type = typeof(TInstance);
                var typeArg1 = typeof(TArg1);
                var typeArg2 = typeof(TArg2);

                var constructor = type.GetConstructors().First();
                if (constructor == null)
                {
                    throw new InvalidOperationException($"Constructor can't be found for type '{type.Name}'");
                }

                var parameters = InstanceUtil.ParametersUtil.GetParametersExpression(new[] { typeArg1, typeArg2 });
                
                var expression = Expression.New(constructor, parameters);
                var expressionBlock = Expression.Block(type, expression);

                return Expression.Lambda<Func<TArg1, TArg2, TInstance>>(expressionBlock, parameters).Compile();
            }
        }
    }
}