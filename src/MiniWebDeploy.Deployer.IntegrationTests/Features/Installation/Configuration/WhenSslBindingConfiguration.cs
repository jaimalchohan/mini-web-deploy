using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;
using System.Linq;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation.Configuration
{
    [TestFixture]
    public class WhenSslBindingConfiguration : SiteTestBase
    {
        private Site _site;

        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            installationConfiguration
                .AndHttpsBinding("myhost3")
                .AndHttpsBinding("myhost4", "9.9.9.9");
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);
            _site = createSite.Install(InstallationConfiguration);

            var bindings = new ConfigureBindings(manager);
            bindings.ConfigureInstalledSite(_site, InstallationConfiguration);
        }

        [Test]
        public void MyHost3BindingHasBeenCreated()
        {
            Assert.That(_site.Bindings.Any(x => x.Host == "myhost3" && x.Protocol == "https"));
        }
    }
}
