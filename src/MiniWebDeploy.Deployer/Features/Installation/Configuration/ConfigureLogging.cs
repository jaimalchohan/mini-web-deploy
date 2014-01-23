using System.IO;
using System.Security.Principal;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;
using MiniWebDeploy.Deployer.Infrastructure;

namespace MiniWebDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureLogging : ConfigurationTaskBase
    {
        private readonly IDirectory _directory;

        public ConfigureLogging(IServerManager serverManager, IDirectory directory)
            : base(serverManager)
        {
            _directory = directory;
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            site.LogFile.Directory = NewOrOriginal(configuration.LogFileDirectory, site.LogFile.Directory);

            if (!configuration.LogFileCreateDirectoryWithElevatedPermissions)
            {
                return;
            }

            if (!_directory.Exists(site.LogFile.Directory))
            {
                _directory.CreateDirectory(site.LogFile.Directory);
            }
            else
            {
                var account = new NTAccount(WindowsIdentity.GetCurrent().Name);
                var existingDirectory = new DirectoryInfo(site.LogFile.Directory);
                var existingDirectorySecurity = existingDirectory.GetAccessControl();
                existingDirectorySecurity.SetOwner(account);
                existingDirectory.SetAccessControl(existingDirectorySecurity);
            }
        }

        private static string NewOrOriginal(string newValue, string oldValue)
        {
            return string.IsNullOrEmpty(newValue) ? oldValue : newValue;
        }
    }
}