using MiniWebDeploy.Deployer.Features.Installation.Installation;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation
{
    [TestFixture]
    public class BindingTests
    {
        [Test]
        public void DefaultProtocolIsHttp()
        {
            var binding = new Binding();
            Assert.AreEqual("http", binding.Protocol);
        }

        [Test]
        public void DefaultIPIsWilcard()
        {
            var binding = new Binding();
            Assert.AreEqual("*", binding.IPAddress);
        }

        [Test]
        public void DefaultPortIs80()
        {
            var binding = new Binding();
            Assert.AreEqual(80, binding.Port);
        }

        [Test]
        public void HostIsSet()
        {
            var binding = new Binding("myhost");
            Assert.AreEqual("myhost", binding.Host);
        }

        [Test]
        public void IpIsSet()
        {
            var binding = new Binding("myhost", "192.168.0.1");
            Assert.AreEqual("192.168.0.1", binding.IPAddress);
        }
    }
}
