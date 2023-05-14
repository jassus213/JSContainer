using System;
using System.Collections.Generic;
using System.Linq;

namespace JSContainer.Common.Tree
{
    public class ScopeTree
    {
        public readonly List<Type> LinkedList = new List<Type>();
        public object ScopeInstance => _scopeInstance;
        private object _scopeInstance;

        public ScopeTree(DiContainer container, Type parrentType)
        {
            LinkedList.Add(parrentType);

            foreach (var parameterExpression in container.BindInfoMap[parrentType].ParameterExpressions)
            {
                var type = parameterExpression.Key;
                LinkedList.Add(parameterExpression.Key);
                if (parameterExpression.Key.IsInterface)
                    type = container.ContractsInfo[type].Last();
                
                LinkedList.AddRange(container.BindInfoMap[type].ParameterExpressions.Keys);
            }
        }

        public void InitializeObject(object instance)
        {
            _scopeInstance = instance;
        }
    }
}