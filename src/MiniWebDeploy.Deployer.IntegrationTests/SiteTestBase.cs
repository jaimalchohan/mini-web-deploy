using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using NUnit.Framework;
using System;
using System.Linq;

namespace MiniWebDeploy.Deployer.IntegrationTests
{
    public class SiteTestBase
    {
        protected string SiteName = "MiniWebDeployIntegrationTestSite";

        [SetUp]
        public void SetUp()
        {
            Given();

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

        protected virtual void Given()
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

                server.CommitChanges();
            }
        }

        protected void CreateExistingSite()
        {
            using (var server = new ServerManager())
            {
                server.Sites.Add(SiteName, Environment.CurrentDirectory, 999);
                server.CommitChanges();
            }
        }
    }
}
