using System;
using System.Collections.Generic;

namespace JSInjector.Binding
{
    public class BindInfo
    {
        public Type CurrentType;
        public readonly Dictionary<Type, List<Type>> TypesMap = new Dictionary<Type, List<Type>>();
        public readonly List<Type> ContractsTypes = new List<Type>();
    }
}