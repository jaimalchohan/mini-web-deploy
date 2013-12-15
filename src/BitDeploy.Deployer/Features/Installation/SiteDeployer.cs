using System.Collections.Generic;
using BitDeploy.Deployer.Features.Installation.ConfigurationTasks;
using BitDeploy.Deployer.Features.Installation.PreInstallationTasks;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation
{
    public class SiteDeployer
    {
        private readonly InstallationConfiguration _installationConfiguration;

        public SiteDeployer(InstallationConfiguration installationConfiguration)
        {
            _installationConfiguration = installationConfiguration;
        }

        public void Deploy()
        {
            using (var serverManager = new ServerManager())
            {
                Deploy(serverManager);
            }
        }

        public void Deploy(ServerManager serverManager)
        {
            new List<IPreInstallationTask>
            {
                new DeleteExistingSite(serverManager),
            }
            .ForEach(x=>x.BeforeInstallation(_installationConfiguration));
            
            var installedSite = serverManager.Sites.Add(_installationConfiguration.SiteName, _installationConfiguration.SitePath, 80);
            installedSite.ServerAutoStart = _installationConfiguration.SiteAutoStart;
            
            new List<IConfigurationTask>
            {
                new ConfigureAppPool(serverManager),
                new ConfigureBindings(serverManager),
                new ConfigureLogging(serverManager),
                new ConfigureAdditionalDirectories(serverManager)
            }
            .ForEach(x => x.ConfigureInstalledSite(installedSite, _installationConfiguration));

            serverManager.CommitChanges();
        }
  

    }
}
