using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Features.Installation;
using Moq;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class SiteInstallerExtensionsTests
    {
        [Test]
        public void ConfigureInstallation_CallsInstallOnISiteInstaller_ExistsToProvideACleanerApiToUsersWhileMaintainingCodeClarity()
        {
            var fakeInstaller = new Mock<ISiteInstaller>();
            var config = new InstallationConfiguration(string.Empty, null);

            fakeInstaller.Object.ConfigureInstallation(config);

            fakeInstaller.Verify(x=>x.Install(config));
        }
    }
}