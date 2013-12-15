using System.IO;
using BitDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureAdditionalDirectories : ConfigurationTaskBase
    {
        public ConfigureAdditionalDirectories(IServerManager serverManager)
            : base(serverManager)
        {
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            foreach (var directory in configuration.AdditionalDirectories)
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}