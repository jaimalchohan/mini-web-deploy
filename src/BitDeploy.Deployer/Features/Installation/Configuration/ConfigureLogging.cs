using System.IO;
using System.Security.Principal;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.ConfigurationTasks
{
    public class ConfigureLogging : ConfigurationTaskBase
    {
        public ConfigureLogging(ServerManager serverManager)
            : base(serverManager)
        {
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            site.LogFile.Directory = NewOrOriginal(configuration.LogFileDirectory, site.LogFile.Directory);

            if (!configuration.LogFileCreateDirectoryWithElevatedPermissions)
            {
                return;
            }

            if (!Directory.Exists(site.LogFile.Directory))
            {
                Directory.CreateDirectory(site.LogFile.Directory);
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