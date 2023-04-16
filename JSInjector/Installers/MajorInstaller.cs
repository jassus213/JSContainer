namespace JSInjector.Installers
{
    public abstract class MajorInstaller : IInstaller
    {
        private DiContainer _container = new DiContainer();

        public DiContainer Container => _container;

        public virtual void Install()
        {
           Container.Bind<DiContainer>().FromResolve(_container);
        }
    }
}