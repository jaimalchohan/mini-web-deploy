using System;
using System.Linq;
using BitDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureAppPool : ConfigurationTaskBase
    {
        public ConfigureAppPool(IServerManager serverManager)
            : base(serverManager)
        {
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration.AppPoolName))
            {
                return;
            }

            if (configuration.AppPoolDeleteExisting)
            {
                var existingAppPool = ServerManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(configuration.AppPoolName, StringComparison.InvariantCultureIgnoreCase));
                ServerManager.ApplicationPools.Remove(existingAppPool);
            }

            site.ApplicationDefaults.ApplicationPoolName = configuration.AppPoolName;

            ConfigureAppPoolIfNotExists(configuration);
        }

        private void ConfigureAppPoolIfNotExists(InstallationConfiguration configuration)
        {
            var existingPool = ServerManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(configuration.AppPoolName));

            if (existingPool != null)
            {
                return;
            }

            var newPool = ServerManager.ApplicationPools.Add(configuration.AppPoolName);
            newPool.ManagedRuntimeVersion = string.IsNullOrEmpty(configuration.AppPoolManagedRuntimeVersion)
                ? newPool.ManagedRuntimeVersion
                : configuration.AppPoolManagedRuntimeVersion;
            newPool.SetAttributeValue("startMode", 1);
        }
    }
}