﻿using System.Collections.Generic;
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
    public class WhenDeletingExistingAppPool : SiteTestBase
    {
        private int _customQueueLength;
        
        protected override void Given(InstallationConfiguration installationConfiguration)
        {
            _customQueueLength = 999;
            CreateExistingSite(_customQueueLength);

            installationConfiguration
                .AndDeleteExistingAppPool()
                .AndStartOnDemand()
                .AndManagedRuntimeVersion("v2.0");
        }

        protected override void When(ServerManagerWrapper manager)
        {
            var existingSite = manager.Sites.Single(x => x.Name == SiteName);

            var appPool = new ConfigureAppPool(manager);
            appPool.ConfigureInstalledSite(existingSite, InstallationConfiguration);
        }

        [Test]
        public void AppPoolWasDeleted_ByComparingQueueLength()
        {
            using (var manager = new ServerManagerWrapper())
            {
                var newAppPoolQueueLength = manager.ApplicationPools.Single(x => x.Name == AppPoolName).QueueLength;
                Assert.AreNotSame(_customQueueLength, newAppPoolQueueLength);
            }
        }

        [Test]
        public void AppPoolIsStartOnDemand()
        {
            using (var manager = new ServerManagerWrapper())
            {
                var startMode = manager.ApplicationPools.Single(x => x.Name == AppPoolName).GetAttributeValue("startMode");
                Assert.AreEqual(AppPoolStartMode.OnDemand, (AppPoolStartMode)startMode);
            }
        }

        [Test]
        public void RuntimeIsv2()
        {
            using (var manager = new ServerManagerWrapper())
            {
                var runtime = manager.ApplicationPools.Single(x => x.Name == AppPoolName).ManagedRuntimeVersion;
                Assert.AreEqual("v2.0", runtime);
            }
        }
    }
}
