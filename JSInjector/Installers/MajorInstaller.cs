namespace JSInjector.Installers
{
    public abstract class MajorInstaller : IInstaller
    {
        private readonly DiContainer _container = new DiContainer();

        protected DiContainer Container => _container;

        public abstract void Install();
    }
}