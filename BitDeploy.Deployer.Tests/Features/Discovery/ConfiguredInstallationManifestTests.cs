using BitDeploy.Deployer.Features.Discovery;
using BitDeploy.Deployer.Features.Installation;
using BitDeploy.Deployer.Tests.Fakes;
using NUnit.Framework;

namespace BitDeploy.Deployer.Tests.Features.Discovery
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

            var dto = new ConfiguredInstallationManifest(config, installer, path);

            Assert.That(dto.InstallationConfiguration, Is.EqualTo(config));
            Assert.That(dto.SourceInstaller, Is.EqualTo(installer));
            Assert.That(dto.Path, Is.EqualTo(path));
        }
    }
}
