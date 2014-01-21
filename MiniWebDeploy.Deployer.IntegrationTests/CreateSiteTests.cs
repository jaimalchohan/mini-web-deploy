using System;
using System.Linq;
using NUnit.Framework;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using MiniWebDeploy.Deployer.Features.Installation;
using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.IntegrationTests
{
    [TestFixture]
    public class CreateSiteTests
    {
        string _siteName;
        CreateSite _createSite;

        [SetUp]
        public void SetUp()
        {
            _siteName = "MiniWebDeployIntegrationTestSite";
            
            DeleteExistingSite(_siteName);

            var manager = new ServerManagerWrapper();

            _createSite = new CreateSite(manager);

            var cfg = new InstallationConfiguration(Environment.CurrentDirectory, null);
            cfg.WithSiteName(_siteName);
            cfg.AndAutoStart();

            _createSite.Install(cfg);

            manager.CommitChanges();
        }

        [TearDown]
        public void TearDown()
        {
            DeleteExistingSite(_siteName);
        }

        private void DeleteExistingSite(string _siteName)
        {
            using(var server = new ServerManager())
            {
                var existing = server.Sites.SingleOrDefault(x => x.Name == _siteName);

                if (existing != null)
                    server.Sites.Remove(existing);

                server.CommitChanges();
            }
        }

        [Test]
        public void NewSiteIsCreated() 
        {
            Assert.NotNull(new ServerManager().Sites.SingleOrDefault(x => x.Name == _siteName));
        }

        [Test]
        public void AutoStartIsSet()
        {
            Assert.IsTrue(new ServerManager().Sites.SingleOrDefault(x => x.Name == _siteName).ServerAutoStart);
        }
    }
}
