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
            new PreInstallationTaskList
            {
                new DeleteExistingSite(serverManager),
            }
            .PerformTasks(_installationConfiguration);
            
            var site = serverManager.Sites.Add(_installationConfiguration.SiteName, _installationConfiguration.SitePath, 80);
            site.ServerAutoStart = _installationConfiguration.SiteAutoStart;

            new ConfigurationTaskList
            {
                new ConfigureAppPool(serverManager),
                new ConfigureBindings(serverManager),
                new ConfigureLogging(serverManager),
                new ConfigureAdditionalDirectories(serverManager)
            }
            .Configure(site, _installationConfiguration);

            serverManager.CommitChanges();
        }
    }
}
