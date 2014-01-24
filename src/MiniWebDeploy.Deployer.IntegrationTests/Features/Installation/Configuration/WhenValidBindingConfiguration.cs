using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation.Configuration
{
    [TestFixture]
    public class WhenValidBindingConfiguration : SiteTestBase
    {
        private Site _site;
        private IEnumerable<Microsoft.Web.Administration.Binding> _existingBindings;

        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            installationConfiguration
                .AndHttpBinding("myhost1")
                .AndHttpBinding("myhost2", "8.8.8.8");
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);
            _site = createSite.Install(InstallationConfiguration);

            var bindings = new ConfigureBindings(manager);
            bindings.ConfigureInstalledSite(_site, InstallationConfiguration);
        }

        [Test]
        public void TwoBindingsAreCreated()
        {
            Assert.AreEqual(2, _site.Bindings.ToArray().Length);
        }

        [Test]
        public void MyHost1BindingHasBeenCreated()
        {
            Assert.That(_site.Bindings.Any(x => x.Host == "myhost1"));
        }

        [Test]
        public void MyHost2BindingHasBeenCreated()
        {
            Assert.That(_site.Bindings.Any(x => x.Host == "myhost2" && x.EndPoint.Address.ToString() == "8.8.8.8"));
        }
    }
}
