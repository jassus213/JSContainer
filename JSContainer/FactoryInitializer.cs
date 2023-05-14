using System;
using System.Linq;
using System.Linq.Expressions;
using JSContainer.Utils.Instance;
using JSContainer.Tests;
using JSContainer.Utils;

namespace JSContainer
{
    internal static class FactoryInitializer
    {
        internal static TFactory Create<TFactory, TResult>()
        {
            return DynamicTypeInstanceFactory<TFactory, TResult>.Create();
        }

        internal static TFactory Create<TFactory, TArgs>(TArgs args)
        {
            return DynamicTypeInstanceFactory<TFactory, TArgs>.Create();
        }

        private static class DynamicTypeInstanceFactory<TFactory, TResult>
        {
            public static readonly Func<TFactory> Create = GenerateFactory();

            private static Func<TFactory> GenerateFactory()
            {
                var type = typeof(TFactory);

                var expressions = new Expression[] { Expression.New(type) };
                var expressionBlock = Expression.Block(type, expressions);

                return Expression.Lambda<Func<TFactory>>(expressionBlock).Compile();
            }
        }

        private static class DynamicTypeInstanceFactory<TFactory, TArgs, TResult>
        {
            public static readonly Func<TArgs, TFactory> Create = GenerateFactory();

            private static Func<TArgs, TFactory> GenerateFactory()
            {
                var type = typeof(TFactory);
                var typeArg1 = typeof(TArgs);

                var constructor = InstanceUtil.ConstructorUtils.GetConstructor(type, ConstructorConventionsSequence.First);

                var parameters = InstanceUtil.ParametersUtil.Map(new[] { typeArg1 });

                var expression = Expression.New(constructor, parameters);
                var expressionBlock = Expression.Block(type, expression);

                return Expression.Lambda<Func<TArgs, TFactory>>(expressionBlock, parameters).Compile();
            }
        }
    }
}