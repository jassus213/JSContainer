using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JSInjector.DiFactories
{
    internal static class FunctionFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TConcrete> CreateFunc<TConcrete>(ConstructorInfo constructorInfo)
        {
            return Expression.Lambda<Func<TConcrete>>(Expression.New(constructorInfo)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TArg1, TConcrete> CreateFunc<TArg1, TConcrete>(ConstructorInfo constructorInfo,
            IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression
                .Lambda<Func<TArg1, TConcrete>>(Expression.New(constructorInfo, parameterExpressions),
                    parameterExpressions).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TArg1, TArg2, TConcrete> CreateFunc<TArg1, TArg2, TConcrete>(
            ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression
                .Lambda<Func<TArg1, TArg2, TConcrete>>(Expression.New(constructorInfo, parameterExpressions),
                    parameterExpressions).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TArg1, TArg2, TArg3, TConcrete> CreateFunc<TArg1, TArg2, TArg3, TConcrete>(
            ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression
                .Lambda<Func<TArg1, TArg2, TArg3, TConcrete>>(Expression.New(constructorInfo, parameterExpressions),
                    parameterExpressions).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TArg1, TArg2, TArg3, TArg4, TConcrete> CreateFunc<TArg1, TArg2, TArg3, TArg4, TConcrete>(
            ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression
                .Lambda<Func<TArg1, TArg2, TArg3, TArg4, TConcrete>>(
                    Expression.New(constructorInfo, parameterExpressions), parameterExpressions).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete> CreateFunc<TArg1, TArg2, TArg3, TArg4, TArg5,
            TConcrete>(ConstructorInfo constructorInfo, IEnumerable<ParameterExpression> parameterExpressions)
        {
            return Expression
                .Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TConcrete>>(
                    Expression.New(constructorInfo, parameterExpressions), parameterExpressions).Compile();
        }
    }
}