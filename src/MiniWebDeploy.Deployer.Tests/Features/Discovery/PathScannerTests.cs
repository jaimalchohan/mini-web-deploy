using System.Collections.Generic;
using System.Reflection;
using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class PathScannerTests
    {
        private string _siteScanPath;
        private Mock<IDiscoverAssembliesThatHaveInstallers> _discoverer;
        private PathScanner _pathScanner;
        private Mock<ILoadAnAssembly> _loader;

        [SetUp]
        public void SetUp()
        {
            _siteScanPath = "c:\\some\\directory";
            _discoverer = new Mock<IDiscoverAssembliesThatHaveInstallers>();
            _loader = new Mock<ILoadAnAssembly>();

            _pathScanner = new PathScanner(_siteScanPath, null, _discoverer.Object, _loader.Object);
        }

        [Test]
        public void Ctor_PoorMansDi_DoesntThrow()
        {
            var scanner = new PathScanner(_siteScanPath, null);

            Assert.That(scanner, Is.Not.Null);
        }

        [Test]
        public void FindFirstAvailableInstaller_Called_SearchesForAssemblies()
        {
            _pathScanner.FindFirstAvailableInstaller();

            _discoverer.Verify(x=>x.FindAssemblies(It.IsAny<string>()));
        }

        [Test]
        public void FindFirstAvailableInstaller_Called_SearchesForAssembliesInSuppliedPathsBinFolder()
        {
            _pathScanner.FindFirstAvailableInstaller();

            _discoverer.Verify(x=>x.FindAssemblies(_siteScanPath + "\\bin"));
        }

        [Test]
        public void FindFirstAvailableInstaller_NoAssembliesFound_ReturnsNoInstallationFoundManifest()
        {
            var manifest = _pathScanner.FindFirstAvailableInstaller();

            Assert.That(manifest, Is.TypeOf<NoInstallationFound>());
        }

        [Test]
        public void FindFirstAvailableInstaller_AssemblyFound_ReturnsManifest()
        {
            var foundAssembly = new AssemblyDetails("", "", typeof(TestInstaller));
            _discoverer.Setup(x => x.FindAssemblies(It.IsAny<string>())).Returns(new List<AssemblyDetails> { foundAssembly });
            _loader.Setup(x => x.Load(It.IsAny<string>())).Returns(Assembly.GetAssembly(typeof(PathScannerTests)));

            var manifest = _pathScanner.FindFirstAvailableInstaller();

            Assert.That(manifest, Is.TypeOf<ConfiguredInstallationManifest>());
        }

        [Test]
        public void FindFirstAvailableInstaller_MultipleAssembliesFound_ReturnsManifestForFirstOne()
        {
            var expectedAssembly = new AssemblyDetails("c:\\path", "expect.this.dll", typeof (TestInstaller));
            var shouldNotCreate = new AssemblyDetails("c:\\path", "do.not.expect.this.dll", typeof (TestInstaller));
            _discoverer.Setup(x => x.FindAssemblies(It.IsAny<string>())).Returns(new List<AssemblyDetails> { expectedAssembly, shouldNotCreate });
            _loader.Setup(x => x.Load("c:\\path\\expect.this.dll")).Returns(Assembly.GetAssembly(typeof(PathScannerTests)));

            var manifest = _pathScanner.FindFirstAvailableInstaller();

            Assert.That(manifest.DiscoveredDetails, Is.EqualTo(expectedAssembly));
        }

        [Test]
        public void FindFirstAvailableInstaller_AssemblyFound_ReturnsConfiguredManifestForAssembly()
        {
            var foundAssembly = new AssemblyDetails("c:\\path", "binary.dll", typeof(TestInstaller));
            _discoverer.Setup(x => x.FindAssemblies(It.IsAny<string>())).Returns(new List<AssemblyDetails> { foundAssembly });
            _loader.Setup(x => x.Load("c:\\path\\binary.dll")).Returns(Assembly.GetAssembly(typeof(PathScannerTests)));

            var manifest = _pathScanner.FindFirstAvailableInstaller();

            Assert.That(manifest.InstallationConfiguration, Is.Not.Null);
            Assert.That(manifest.Path, Is.EqualTo(_siteScanPath));
            Assert.That(manifest.SourceInstaller, Is.InstanceOf<ISiteInstaller>());
        }
    }
}
