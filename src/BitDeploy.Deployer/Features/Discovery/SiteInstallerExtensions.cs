namespace BitDeploy.Deployer.Features.Discovery
{
    public static class SiteInstallerExtensions
    {
        public static void ConfigureInstallation(this ISiteInstaller src, IInstallationConfiguration x)
        {
            src.Install(x);
        }
    }
}