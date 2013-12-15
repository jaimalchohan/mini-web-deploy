using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.PreInstallationTasks
{
    public interface IPreInstallationTask
    {
        void BeforeInstallation(InstallationConfiguration configuration);
    }
}