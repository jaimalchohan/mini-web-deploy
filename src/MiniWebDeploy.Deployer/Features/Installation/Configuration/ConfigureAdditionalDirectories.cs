using System.IO;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Infrastructure;

namespace MiniWebDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureAdditionalDirectories : ConfigurationTaskBase
    {
        private IDirectory _directory;

        public ConfigureAdditionalDirectories(IServerManager serverManager, IDirectory directory)
            : base(serverManager)
        {
            _directory = directory;
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            foreach (var directory in configuration.AdditionalDirectories)
            {
                _directory.CreateDirectory(directory);
            }
        }
    }    
}