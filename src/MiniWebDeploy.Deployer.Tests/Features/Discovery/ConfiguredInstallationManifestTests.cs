using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Tests.Fakes;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class ConfiguredInstallationManifestTests
    {
        [Test]
        public void Ctor_StoresProvidedArguments()
        {
            var config = new InstallationConfiguration("path");
            var installer = new TestInstaller();
            const string path = "path";
            var discoveredDetails = new AssemblyDetails(path, "binary.dll", typeof (TestInstaller));

            var dto = new ConfiguredInstallationManifest(config, installer, path, discoveredDetails);

            Assert.That(dto.InstallationConfiguration, Is.EqualTo(config));
            Assert.That(dto.SourceInstaller, Is.EqualTo(installer));
            Assert.That(dto.Path, Is.EqualTo(path));
            Assert.That(dto.DiscoveredDetails, Is.EqualTo(discoveredDetails));
        }
    }
}
