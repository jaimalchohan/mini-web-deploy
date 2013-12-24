using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Tests.Fakes;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class AssemblyDetailsTests
    {
        [Test]
        public void Ctor_StoresPathsProvided()
        {
            var instance = new AssemblyDetails("one", "two", typeof(TestInstaller));

            Assert.That(instance.Path, Is.EqualTo("one"));
            Assert.That(instance.BinaryPath, Is.EqualTo("two"));
        }
    }
}
