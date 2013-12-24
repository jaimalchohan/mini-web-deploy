namespace MiniWebDeploy.Deployer.Features.Installation.PreInstallation
{
    public interface IPreInstallationTask
    {
        void BeforeInstallation(InstallationConfiguration configuration);
    }
}