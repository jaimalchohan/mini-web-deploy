using System;
using System.Linq;
using NUnit.Framework;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using MiniWebDeploy.Deployer.Features.Installation;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation
{
    [TestFixture]
    public class CreateSiteTests : SiteTestBase
    {
        
        protected override void Given()
        {
 	        DeleteExistingSite();
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);

            var cfg = new InstallationConfiguration(Environment.CurrentDirectory, null);
            cfg.WithSiteName(SiteName);
            cfg.AndAutoStart();

            createSite.Install(cfg);
        }

        [Test]
        public void NewSiteIsCreated() 
        {
            Assert.NotNull(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName));
        }

        [Test]
        public void AutoStartIsSet()
        {
            Assert.IsTrue(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName).ServerAutoStart);
        }
    }
}
