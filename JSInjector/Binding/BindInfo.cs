using System;
using System.Collections.Generic;

namespace JSInjector.Binding
{
    public class BindInfo
    {
        internal readonly Dictionary<Type, List<Type>> TypesMap = new Dictionary<Type, List<Type>>();
        internal readonly List<Type> ContractsTypes = new List<Type>();

        public BindInfo(Type currentType)
        {
            TypesMap.Add(currentType, new List<Type>());
        }
    }
}