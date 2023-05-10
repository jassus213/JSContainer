using JSInjector.Binding.BindInfo;
using JSInjector.Common.Enums;

namespace JSInjector.Installers.MajorInstaller
{
    public abstract class MajorInstaller : IMajorInstaller
    {
        protected DiContainer Container => _container;
        private readonly DiContainer _container = new DiContainer();

        public virtual void Install()
        {
            Container.Bind<DiContainer>().FromResolve(_container, BindType.InterfacesAndSelfTo).AsSingleton();
        }
    }
}