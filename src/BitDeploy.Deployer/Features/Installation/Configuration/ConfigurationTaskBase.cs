using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.ConfigurationTasks
{
    public abstract class ConfigurationTaskBase : IConfigurationTask
    {
        protected readonly ServerManager ServerManager;

        protected ConfigurationTaskBase(ServerManager serverManager)
        {
            ServerManager = serverManager;
        }

        public abstract void ConfigureInstalledSite(Site site, InstallationConfiguration configuration);
    }
}