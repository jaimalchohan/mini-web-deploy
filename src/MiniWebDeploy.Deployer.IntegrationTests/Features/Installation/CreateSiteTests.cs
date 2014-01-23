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

        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            installationConfiguration.AndAutoStart();
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);
            createSite.Install(InstallationConfiguration);
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
