using System;
using System.Linq;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.PreInstallation
{
    [TestFixture]
    public class AndSiteDoesNotExist : SiteTestBase
    {
        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            installationConfiguration = new InstallationConfiguration(Environment.CurrentDirectory, null);
            installationConfiguration.AndDeleteExistingSite();
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var deleteSite = new DeleteExistingSite(manager);

            deleteSite.BeforeInstallation(InstallationConfiguration);
        }

        [Test]
        public void ExistingSiteDoeNotExist_AndExceptionIsNotThrown()
        {
            Assert.Null(new ServerManager().Sites.SingleOrDefault(x => x.Name == SiteName));
        }
    }
}
