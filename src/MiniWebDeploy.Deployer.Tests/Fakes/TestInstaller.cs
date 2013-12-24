using System;

namespace MiniWebDeploy.Deployer.Tests.Fakes
{
    public class TestInstaller : ISiteInstaller
    {
        private readonly Action<IInstallationConfiguration> _setupFake;

        public TestInstaller()
        {
        }

        public TestInstaller(Action<IInstallationConfiguration> setupFake = null)
        {
            _setupFake = setupFake ?? (_ => { });
        }

        public void Install(IInstallationConfiguration x)
        {
            _setupFake(x);
        }
    }
}