using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;

namespace MiniWebDeploy.Deployer.Features.Installation.PreInstallation
{
    public abstract class PreInstallationTaskBase : IPreInstallationTask
    {
        protected IServerManager ServerManager;

        protected PreInstallationTaskBase(IServerManager serverManager)
        {
            ServerManager = serverManager;
        }

        public abstract void BeforeInstallation(InstallationConfiguration configuration);
    }
}