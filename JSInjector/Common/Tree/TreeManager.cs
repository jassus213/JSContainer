﻿using System;
using System.Linq;
using JSInjector.Common.Enums;

namespace JSInjector.Common.Tree
{ 
    internal static class TreeManager
    {
        internal static void InitializeTree(DiContainer container, Type typeConcrete)
        {
            foreach (var parameterExpression in container.BindInfoMap[typeConcrete].ParameterExpressions)
            {
                var currentTypeExpression = parameterExpression.Key;

                if (parameterExpression.Key.IsInterface)
                    currentTypeExpression = container.ContractsInfo[currentTypeExpression].Last();


                var keyValuePair = container.ScopedTree.FirstOrDefault(x => x.Key.Contains(typeConcrete));

                if (container.BindInfoMap[currentTypeExpression].LifeCycle == LifeCycle.Scoped &&
                    keyValuePair.Value == null)
                {
                    var tree = TreeFactory.CreateTree(container, typeConcrete);
                    container.ScopedTree.Add(tree.LinkedList, tree);
                    break;
                }
            }
        }
    }
}