using BitDeploy.Deployer.Features.Discovery;
using BitDeploy.Deployer.Features.Installation;
using Moq;
using NUnit.Framework;

namespace BitDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class SiteInstallerExtensionsTests
    {
        [Test]
        public void ConfigureInstallation_CallsInstallOnISiteInstaller_ExistsToProvideACleanerApiToUsersWhileMaintainingCodeClarity()
        {
            var fakeInstaller = new Mock<ISiteInstaller>();
            var config = new InstallationConfiguration(string.Empty);

            fakeInstaller.Object.ConfigureInstallation(config);

            fakeInstaller.Verify(x=>x.Install(config));
        }
    }
}