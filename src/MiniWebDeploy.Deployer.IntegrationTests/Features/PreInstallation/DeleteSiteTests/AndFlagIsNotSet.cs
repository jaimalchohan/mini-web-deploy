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
    public class AndFlagIsNotSet : SiteTestBase
    {
        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            CreateExistingSite();

            installationConfiguration.WithSiteName("SiteOtherNameWhichShouldNotBeCreated");
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var deleteSite = new DeleteExistingSite(manager);

            deleteSite.BeforeInstallation(InstallationConfiguration);
        }

        [Test]
        public void ExistingSiteIsNotDeleted()
        {
            Assert.NotNull(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName));
        }
    }
}
