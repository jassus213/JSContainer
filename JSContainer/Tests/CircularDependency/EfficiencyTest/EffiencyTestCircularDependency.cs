using JSContainer.Installers.MajorInstaller;
using NUnit.Framework;

namespace JSContainer.Tests.CircularDependency.EfficiencyTest
{
    public class EffiencyTestCircularDependency
    {
        private readonly IMajorInstaller _majorInstaller;

        public EffiencyTestCircularDependency(IMajorInstaller installer)
        {
            _majorInstaller = installer;
            Test();
        }
        
        private void Test()
        {
            _majorInstaller.Install();
        }
    }
}