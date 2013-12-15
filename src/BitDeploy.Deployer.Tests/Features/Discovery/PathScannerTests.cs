using BitDeploy.Deployer.Features.Discovery;
using Moq;
using NUnit.Framework;

namespace BitDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class PathScannerTests
    {
        private string _siteScanPath;
        private Mock<IDiscoverAssembliesThatHaveInstallers> _discoverer;
        private PathScanner _pathScanner;

        [SetUp]
        public void SetUp()
        {
            _siteScanPath = "c:\\some\\directory";
            _discoverer = new Mock<IDiscoverAssembliesThatHaveInstallers>();

            _pathScanner = new PathScanner(_siteScanPath, _discoverer.Object);
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
    }
}
