using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Configuration
{
    public interface IConfigurationTask
    {
        void ConfigureInstalledSite(Site site, InstallationConfiguration configuration);
    }
}