using BitDeploy.Deployer.Features.Installation;

namespace BitDeploy.Deployer.Features.Discovery
{
    public class ConfiguredInstallationManifest
    {
        public InstallationConfiguration InstallationConfiguration { get; set; }
        public ISiteInstaller SourceInstaller { get; set; }
        public string Path { get; set; }

        public ConfiguredInstallationManifest(InstallationConfiguration installationConfiguration, ISiteInstaller sourceInstaller, string path)
        {
            InstallationConfiguration = installationConfiguration;
            SourceInstaller = sourceInstaller;
            Path = path;
        }
    }
}