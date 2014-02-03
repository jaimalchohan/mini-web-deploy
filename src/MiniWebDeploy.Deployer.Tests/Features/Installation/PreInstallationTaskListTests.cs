using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;
using Moq;
using NUnit.Framework;

namespace MiniWebDeploy.Deployer.Tests.Features.Installation
{
    [TestFixture]
    public class PreInstallationTaskListTests
    {
        [Test]
        public void BeforeInstallationIsCalledForAllTasks()
        {
            var task = new Mock<IPreInstallationTask>();

            var taskList = new PreInstallationTaskList();
            taskList.Add(task.Object);
            taskList.Add(task.Object);

            taskList.PerformTasks(new InstallationConfiguration(null, null));

            task.Verify(x => x.BeforeInstallation(It.IsAny<InstallationConfiguration>()), Times.Exactly(2));
        }
    }
}
