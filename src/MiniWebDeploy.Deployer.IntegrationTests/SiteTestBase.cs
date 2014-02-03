using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;
using System;
using System.Linq;

namespace MiniWebDeploy.Deployer.IntegrationTests
{
    public class SiteTestBase
    {
        protected readonly string SiteName = "MiniWebDeployIntegrationTestSite";
        protected readonly string AppPoolName = "MiniWebDeployIntegrationTestSiteAppPool";
        protected InstallationConfiguration InstallationConfiguration { get; private set; }

        [SetUp]
        public void SetUp()
        {
            DeleteExistingSite();

            InstallationConfiguration = new InstallationConfiguration(Environment.CurrentDirectory, null);
            InstallationConfiguration.WithSiteName(SiteName);
            InstallationConfiguration.WithAppPool(AppPoolName);

            Given(InstallationConfiguration);

            using (var manager = new ServerManagerWrapper())
            {
                When(manager);

                manager.CommitChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            DeleteExistingSite();
        }

        protected virtual void Given(InstallationConfiguration installationConfiguration)
        {

        }

        protected virtual void When(ServerManagerWrapper manager)
        {

        }

        protected void DeleteExistingSite()
        {
            using (var server = new ServerManager())
            {
                var existing = server.Sites.SingleOrDefault(x => x.Name == SiteName);

                if (existing != null)
                    server.Sites.Remove(existing);

                var existingAppPool = server.ApplicationPools.SingleOrDefault(x => x.Name == AppPoolName);

                if (existingAppPool != null)
                    server.ApplicationPools.Remove(existingAppPool);

                server.CommitChanges();
            }
        }

        protected void CreateExistingSite(long? customQueueLength = null)
        {
            using (var server = new ServerManager())
            {
                var site = server.Sites.Add(SiteName, Environment.CurrentDirectory, 999);
                var appPool = server.ApplicationPools.Add(AppPoolName);
                appPool.QueueLength = customQueueLength ?? appPool.QueueLength;
                site.ApplicationDefaults.ApplicationPoolName = AppPoolName;
                server.CommitChanges();
            }
        }
    }
}
