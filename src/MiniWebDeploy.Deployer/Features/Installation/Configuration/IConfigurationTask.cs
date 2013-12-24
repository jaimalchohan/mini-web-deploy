using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Features.Installation.Configuration
{
    public interface IConfigurationTask
    {
        void ConfigureInstalledSite(Site site, InstallationConfiguration configuration);
    }
}