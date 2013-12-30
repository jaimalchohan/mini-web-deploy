using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class DiscoverAssembliesThatHaveInstallersTests
    {
        private string _siteScanPath;
        private IDiscoverAssembliesThatHaveInstallers _discoverer;
        private Mock<ILoadAnAssembly> _loader;
        private Mock<IEnumerateAssemblies> _assemblyEnumerator;

        [SetUp]
        public void SetUp()
        {
            _siteScanPath = "c:\\some\\directory";
            _loader = new Mock<ILoadAnAssembly>();
            _assemblyEnumerator = new Mock<IEnumerateAssemblies>();
            _discoverer = new DiscoverAssembliesThatHaveInstallers(_loader.Object, _assemblyEnumerator.Object);
        }

        [Test]
        public void EmptyWhen_NoAssembliesFound()
        {
            _assemblyEnumerator.Setup(x => x.EnumerateFrom(_siteScanPath))
                .Returns(new string[] { });

            Assert.IsEmpty(_discoverer.FindAssemblies(_siteScanPath));
        }

        [Test]
        public void NotEmpty_WhenAssembliesFound()
        {
            var testInstallerAssembly = typeof(TestInstaller).Assembly;
            var binaryPath = typeof(TestInstaller).Assembly.Location;

            _assemblyEnumerator.Setup(x => x.EnumerateFrom(_siteScanPath))
                .Returns((new[] { binaryPath }));

            _loader.Setup(x => x.ReflectionOnlyLoadFrom(binaryPath))
                .Returns(testInstallerAssembly);

            Assert.IsNotEmpty(_discoverer.FindAssemblies(_siteScanPath));
        }
    }
}
