using System;

namespace JSInjector.Common.TypeInstancePair
{
    public struct TypeInstancePair
    {
        public readonly Type Type => _type;
        private Type _type;
        public readonly object Instance => _instance;
        private object _instance;

        private readonly bool _isFilled;

        public TypeInstancePair(Type type, object instance)
        {
            _isFilled = false;
            
            if (type != null && instance != null)
                _isFilled = true;

            _type = type;
            _instance = instance;
        }

        public void FillPair(object instance)
        {
            if (instance == null || _isFilled) 
                return;
            
            _instance = instance;
            _type = instance.GetType();
        }
    }
}