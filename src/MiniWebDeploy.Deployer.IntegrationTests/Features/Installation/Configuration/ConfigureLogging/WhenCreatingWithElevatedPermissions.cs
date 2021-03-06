﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;
using Moq;
using MiniWebDeploy.Deployer.Infrastructure;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation.Configuration
{
    [TestFixture]
    public class WhenCreatingWithElevatedPermissions : SiteTestBase
    {
        private Site _site;
        private Mock<IDirectory> _directoryMock;
        public string _directory;

        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            _directory = "c:\\mockdir";

            installationConfiguration.WithLogFile()
                .AndDirectory(_directory)
                .AndCreateDirectoryWithElevatedPermissions();

            _directoryMock = new Mock<IDirectory>();
            _directoryMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);
            _site = createSite.Install(InstallationConfiguration);

            var logging = new ConfigureLogging(manager, _directoryMock.Object);
            logging.ConfigureInstalledSite(_site, InstallationConfiguration);
        }

        [Test]
        public void DirectoryCreated()
        {
            _directoryMock.Verify(x => x.CreateDirectory(_directory), Times.Once);
        }

        
    }
}
