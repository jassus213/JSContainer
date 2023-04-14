using JSInjector.Installers;
using TestProject;

namespace Progrma
{
    public class StartInstaller : MajorInstaller
    {
        public override void Install()
        {
            Container.Bind<Company>();
        }
    }
}