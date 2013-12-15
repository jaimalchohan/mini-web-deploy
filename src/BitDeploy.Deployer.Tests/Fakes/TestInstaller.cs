using System;

namespace BitDeploy.Deployer.Tests.Fakes
{
    public class TestInstaller : ISiteInstaller
    {
        private readonly Action<IInstallationConfiguration> _setupFake;

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