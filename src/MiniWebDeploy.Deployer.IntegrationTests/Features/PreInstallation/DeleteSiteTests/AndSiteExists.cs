using System;
using System.Linq;
using NUnit.Framework;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using MiniWebDeploy.Deployer.Features.Installation;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.PreInstallation
{
    [TestFixture]
    public class AndSiteExists : SiteTestBase
    {
        protected override void Given()
        {
            DeleteExistingSite();
            CreateExistingSite();
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
        public void ExistingSiteIsDeleted()
        {
            Assert.Null(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName));
        }
    }
}
