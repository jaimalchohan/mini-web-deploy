using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Infrastructure;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation
{
    [TestFixture]
    public class SiteDeployerTests
    {
        private SiteDeployer _siteDeployer;
        private Mock<IServerManager> _serverManager;
        
        [SetUp]
        public void SetUp()
        {
            _serverManager = new Mock<IServerManager>(); 
            var directory = new Mock<IDirectory>();

            _serverManager.SetupGet(x => x.Sites).Returns(new ServerManager().Sites);

            var cfg = new InstallationConfiguration(null, null);
            cfg.WithSiteName("SiteDeployerIntegrationTest");


            _siteDeployer = new SiteDeployer(_serverManager.Object, cfg, directory.Object);
        }

        [Test]
        public void ChangesAreCommittedToServerManager()
        {
            _siteDeployer.Deploy();

            _serverManager.Verify(x => x.CommitChanges());
        }
    }
}
