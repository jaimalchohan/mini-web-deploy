using MiniWebDeploy.Deployer.Features.Installation;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation
{
    [TestFixture]
    public class InstallationConfigurationTests
    {
        InstallationConfiguration _cfg;

        [SetUp]
        public void SetUp()
        {
            _cfg = new InstallationConfiguration("", null);
        }

        [Test]
        public void SiteNameHasDefault()
        {
            Assert.AreEqual("Default Website", _cfg.SiteName);
        }

    }
}
