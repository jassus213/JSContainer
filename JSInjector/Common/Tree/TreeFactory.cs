using System;

namespace JSInjector.Common.Tree
{
    public static class TreeFactory
    {
        public static readonly Func<DiContainer, Type, ScopeTree> CreateTree = (container, currentType) =>
            new ScopeTree(container, currentType);
    }
}