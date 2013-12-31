using System.Collections.Generic;
using System.Reflection;
using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Tests.Fakes;
using Moq;
using NUnit.Framework;
using System.IO;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class DiscoverAssembliesThatHaveInstallersTests
    {
        private string _siteScanPath;
        private IDiscoverAssembliesThatHaveInstallers _discoverer;
        private Mock<ILoadAnAssembly> _loader;

        [SetUp]
        public void SetUp()
        {
            _siteScanPath = "c:\\some\\directory";
            _loader = new Mock<ILoadAnAssembly>();

            _discoverer = new DiscoverAssembliesThatHaveInstallers(_loader.Object);
        }

        [Test]
        public void FindFirstAvailableInstaller_Called_SearchesForAssemblies()
        {
            _discoverer.FindAssemblies(Path.GetDirectoryName(typeof(TestInstaller).Assembly.Location));
        }
    }
}
