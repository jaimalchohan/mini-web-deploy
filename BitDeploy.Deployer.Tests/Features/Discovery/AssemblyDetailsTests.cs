using BitDeploy.Deployer.Features.Discovery;
using NUnit.Framework;

namespace BitDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class AssemblyDetailsTests
    {
        [Test]
        public void Ctor_StoresPathsProvided()
        {
            var instance = new AssemblyDetails("one", "two");

            Assert.That(instance.Path, Is.EqualTo("one"));
            Assert.That(instance.BinaryPath, Is.EqualTo("two"));
        }
    }
}
