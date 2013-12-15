using BitDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Configuration
{
    public abstract class ConfigurationTaskBase : IConfigurationTask
    {
        protected readonly IServerManager ServerManager;

        protected ConfigurationTaskBase(IServerManager serverManager)
        {
            ServerManager = serverManager;
        }

        public abstract void ConfigureInstalledSite(Site site, InstallationConfiguration configuration);
    }
}