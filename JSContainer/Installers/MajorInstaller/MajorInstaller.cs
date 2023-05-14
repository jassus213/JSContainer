using JSContainer.Common.Enums;

namespace JSContainer.Installers.MajorInstaller
{
    public abstract class MajorInstaller : IMajorInstaller
    {
        public DiContainer Container => _container;
        private readonly DiContainer _container = new DiContainer();

        public virtual void Install()
        {
            Container.Bind<DiContainer>().FromResolve(_container, BindType.InterfacesAndSelfTo).AsSingleton();
        }
    }
}