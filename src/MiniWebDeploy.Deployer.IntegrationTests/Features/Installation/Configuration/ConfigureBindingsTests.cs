using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniWebDeploy.Deployer.IntegrationTests.Features.Installation.Configuration
{
    [TestFixture]
    public class WhenNoBindingConfiguration : SiteTestBase
    {
        private Site _site;
        private IEnumerable<Microsoft.Web.Administration.Binding> _existingBindings;

        protected override void Given(InstallationConfiguration installationConfiguration)
        {

        }

        protected override void When(ServerManagerWrapper manager)
        {
            var createSite = new CreateSite(manager);
            _site = createSite.Install(InstallationConfiguration);

           _existingBindings = _site.Bindings.Select(x => x);

            var bindings = new ConfigureBindings(manager);
            bindings.ConfigureInstalledSite(_site, InstallationConfiguration);
        }

        [Test]
        public void SameNumberOfBindings()
        {
            Assert.AreEqual(_existingBindings.Count(), _site.Bindings.ToArray().Length);
        }

        [Test]
        public void AllBindingsAreTheSame()
        {
            Assert.That(_existingBindings.All(x => _site.Bindings.Contains(x)));
        }
    }
}
