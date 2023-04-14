namespace JSInjector.Binding
{
    public class ConcreteIdBinder<TContract>
    {
        private readonly DiContainer _diContainer;
        private readonly BindInfo _bindInfo;
        
        public ConcreteIdBinder(DiContainer diContainer, BindInfo bindInfo)
        {
            _diContainer = diContainer;
            _bindInfo = bindInfo;
        }
        
        public void To<TConcrete>()
        {
            var tConcreteInfo = _diContainer.BindInfoMap[typeof(TConcrete)];
            tConcreteInfo.TypesMap[tConcreteInfo.CurrentType].Add(typeof(TContract));
        }
    }
}