using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using Moq;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation
{
    [TestFixture]
    public class ConfigurationTaskListTests
    {
        [Test]
        public void ConfigureInstalledSiteIsCalledForAllTasks()
        {
            var confTask = new Mock<IConfigurationTask>();

            var taskList = new ConfigurationTaskList();
            taskList.Add(confTask.Object);
            taskList.Add(confTask.Object);

            taskList.Configure(null, new InstallationConfiguration(null, null));

            confTask.Verify(x => x.ConfigureInstalledSite(null, It.IsAny<InstallationConfiguration>()), Times.Exactly(2));
        }
    }
}
