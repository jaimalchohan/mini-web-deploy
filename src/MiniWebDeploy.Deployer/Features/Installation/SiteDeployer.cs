using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using MiniWebDeploy.Deployer.Features.Installation.Installation;
using MiniWebDeploy.Deployer.Features.Installation.PreInstallation;
using MiniWebDeploy.Deployer.Infrastructure;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;

namespace MiniWebDeploy.Deployer.Features.Installation
{
    public class SiteDeployer
    {
        private readonly IServerManager _serverManager;
        private readonly InstallationConfiguration _installationConfiguration;
        private readonly IDirectory _directory;

        private readonly PreInstallationTaskList _preInstall;
        private readonly CreateSite _installation;
        private readonly ConfigurationTaskList _configuration;

        public SiteDeployer(IServerManager serverManager, InstallationConfiguration installationConfiguration, IDirectory directory)
        {
            _serverManager = serverManager;
            _installationConfiguration = installationConfiguration;
            _directory = directory;

            _preInstall = new PreInstallationTaskList
            {
                new DeleteExistingSite(_serverManager),
            };

            _installation = new CreateSite(_serverManager);

            _configuration = new ConfigurationTaskList
            {
                new ConfigureAppPool(_serverManager),
                new ConfigureBindings(_serverManager),
                new ConfigureLogging(_serverManager, _directory),
                new ConfigureAdditionalDirectories(_serverManager, _directory)
            };
        }

        public void Deploy()
        {
            _preInstall.PerformTasks(_installationConfiguration);
            var site = _installation.Install(_installationConfiguration);
            _configuration.Configure(site, _installationConfiguration);

            _serverManager.CommitChanges();
        }
    }
}