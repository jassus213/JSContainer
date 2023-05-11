using JSInjector.Installers.MajorInstaller;
using NUnit.Framework;

namespace JSInjector.Tests.CircularDependency.EfficiencyTest
{
    public class EffiencyTestCd
    {
        private readonly IMajorInstaller _majorInstaller;

        public EffiencyTestCd(IMajorInstaller installer)
        {
            _majorInstaller = installer;
            Test();
        }
        
        public void Test()
        {
            _majorInstaller.Install();
        }
    }
}