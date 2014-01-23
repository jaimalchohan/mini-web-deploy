using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Infrastructure;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation.Configuration
{
    [TestFixture]
    class ConfigureAdditionalDirectoriesTests
    {
        ConfigureAdditionalDirectories _cfg;
        Mock<IDirectory> _directoryMock;
        Mock<IServerManager> _serverManagerMock;

        [SetUp]
        public void SetUp()
        {
            _directoryMock = new Mock<IDirectory>();
            _serverManagerMock = new Mock<IServerManager>();

            _cfg = new ConfigureAdditionalDirectories(_serverManagerMock.Object, _directoryMock.Object);
        }

        [Test]
        public void AllDirectoriesAreCreated()
        {
            var icfg = new InstallationConfiguration(null, null);
            icfg.WithDirectory("DirectoryA");
            icfg.WithDirectory("DirectoryB");

            _cfg.ConfigureInstalledSite(null, icfg);

            _directoryMock.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
