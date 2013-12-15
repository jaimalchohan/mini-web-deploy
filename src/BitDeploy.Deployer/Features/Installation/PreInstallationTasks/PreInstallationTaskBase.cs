using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.PreInstallationTasks
{
    public abstract class PreInstallationTaskBase : IPreInstallationTask
    {
        protected ServerManager ServerManager;

        protected PreInstallationTaskBase(ServerManager serverManager)
        {
            ServerManager = serverManager;
        }

        public abstract void BeforeInstallation(InstallationConfiguration configuration);
    }
}