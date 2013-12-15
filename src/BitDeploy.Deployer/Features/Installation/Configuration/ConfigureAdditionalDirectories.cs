using System.IO;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureAdditionalDirectories : ConfigurationTaskBase
    {
        public ConfigureAdditionalDirectories(ServerManager serverManager)
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