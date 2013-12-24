using System.Collections.Generic;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;

namespace MiniWebDeploy.Deployer.Features.Installation
{
    public class PreInstallationTaskList : List<IPreInstallationTask>
    {
        public void PerformTasks(InstallationConfiguration configuration)
        {
            foreach (var task in this)
            {
                task.BeforeInstallation(configuration);
            }
        }
    }
}