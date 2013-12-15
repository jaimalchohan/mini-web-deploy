using System.Collections.Generic;
using BitDeploy.Deployer.Features.Installation.PreInstallation;

namespace BitDeploy.Deployer.Features.Installation
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