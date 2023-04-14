using JSInjector.Installers;
using TestProject;

namespace Progrma
{
    public class StartInstaller : MajorInstaller
    {
        public override void Install()
        {
            Container.Bind<Manager>();
            Container.Bind<Company>().To<Manager>();
            Container.Bind<Tester>().To<Manager>();
            Container.Instantiate(Container);
        }
    }
}