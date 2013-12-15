using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.ConfigurationTasks
{
    public interface IConfigurationTask
    {
        void ConfigureInstalledSite(Site site, InstallationConfiguration configuration);
    }
}