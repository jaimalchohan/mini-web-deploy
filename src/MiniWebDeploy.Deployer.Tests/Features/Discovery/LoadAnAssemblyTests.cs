using System.Reflection;
using MiniWebDeploy.Deployer.Features.Discovery;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Discovery
{
    [TestFixture]
    public class LoadAnAssemblyTests
    {
        [Test]
        public void Ctor_DoesntThrow()
        {
            var loader = new LoadAnAssembly();

            Assert.That(loader, Is.Not.Null);
        }

        [Test]
        public void CanLoadAnAssemblyThatExists()
        {
            var thisActualTestAssembly = Assembly.GetAssembly(typeof(LoadAnAssemblyTests));
            var loader = new LoadAnAssembly();

            var assemblyInception = loader.Load(thisActualTestAssembly.Location);

            Assert.That(assemblyInception, Is.EqualTo(thisActualTestAssembly));
        }
    }
}
