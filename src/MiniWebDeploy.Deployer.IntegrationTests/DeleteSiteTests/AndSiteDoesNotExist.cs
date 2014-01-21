using System;
using System.Linq;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.IntegrationTests
{
    [TestFixture]
    public class AndSiteDoesNotExist : SiteTestBase
    {
        protected override void Given()
        {
            DeleteExistingSite();
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var deleteSite = new DeleteExistingSite(manager);

            var cfg = new InstallationConfiguration(Environment.CurrentDirectory, null);
            cfg.WithSiteName(SiteName);
            cfg.AndDeleteExistingSite();

            deleteSite.BeforeInstallation(cfg);
        }

        [Test]
        public void ExistingSiteDoeNotExist_AndExceptionIsNotThrown()
        {
            Assert.Null(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName));
        }
    }
}
