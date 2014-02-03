using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Moq;
using NUnit.Framework;
using System;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation.Configuration
{
    [TestFixture]
    public class ConfigureAppPoolTests
    {
        ConfigureAppPool _cfg;
        Mock<IServerManager> _serverManagerMock;

        [SetUp]
        public void SetUp()
        {
            _serverManagerMock = new Mock<IServerManager>();
            _cfg = new ConfigureAppPool(_serverManagerMock.Object);
        }

        [Test]
        public void NothingHappensWhenAppPoolNameIsEmpty()
        {
            _cfg.ConfigureInstalledSite(null, new InstallationConfiguration(null, null));

        }
    }
}
