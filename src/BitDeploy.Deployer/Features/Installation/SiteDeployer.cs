using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation
{
    public class SiteDeployer
    {
        private readonly InstallationConfiguration _installationConfiguration;

        public SiteDeployer(InstallationConfiguration installationConfiguration)
        {
            _installationConfiguration = installationConfiguration;
        }

        public void Deploy()
        {
            using (var serverManager = new ServerManager())
            {

                if(_installationConfiguration.SiteDeleteExisting)
                {
                    var existingSite = serverManager.Sites.SingleOrDefault(x => x.Name.Equals(_installationConfiguration.SiteName, StringComparison.InvariantCultureIgnoreCase));

                    if(existingSite != null)
                    {
                        serverManager.Sites.Remove(existingSite);
                    }
                }

                var mySite = serverManager.Sites.Add(_installationConfiguration.SiteName, _installationConfiguration.SitePath, 80);
                mySite.ServerAutoStart = _installationConfiguration.SiteAutoStart;

                if (!string.IsNullOrEmpty(_installationConfiguration.AppPoolName))
                {
                    if (_installationConfiguration.AppPoolDeleteExisting)
                    {
                        var existingAppPool = serverManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(_installationConfiguration.AppPoolName, StringComparison.InvariantCultureIgnoreCase));
                        serverManager.ApplicationPools.Remove(existingAppPool);
                    }

                    mySite.ApplicationDefaults.ApplicationPoolName = _installationConfiguration.AppPoolName;

                    ConfigureAppPoolIfNotExists(serverManager);
                }

                ConfigureBindings(mySite);
                ConfigureLogging(mySite);
                ConfigureAdditionalDirectories();

                serverManager.CommitChanges();
            }
        } 

        public void ConfigureAppPoolIfNotExists(ServerManager serverManager)
        {
            var existingPool = serverManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(_installationConfiguration.AppPoolName));

            if (existingPool == null)
            {
                var newPool = serverManager.ApplicationPools.Add(_installationConfiguration.AppPoolName);
                newPool.ManagedRuntimeVersion = string.IsNullOrEmpty(_installationConfiguration.AppPoolManagedRuntimeVersion) 
                    ? newPool.ManagedRuntimeVersion
                    : _installationConfiguration.AppPoolManagedRuntimeVersion;
                newPool.SetAttributeValue("startMode", 1);
            }
        }

        private void ConfigureBindings(Site mySite)
        {
            if(_installationConfiguration.Bindings.Any())
            {
                mySite.Bindings.Clear();
                
                foreach(var binding in _installationConfiguration.Bindings)
                {
                    var b  = mySite.Bindings.CreateElement();
                    b.Protocol = binding.Protocol;
                    b.BindingInformation = string.Format("{0}:{1}:{2}", binding.IPAddress, binding.Port, binding.Host);
                    mySite.Bindings.Add(b);
                }
            }
        }

        private void ConfigureLogging(Site mySite)
        {
            mySite.LogFile.Directory = NewOrOriginal(_installationConfiguration.LogFileDirectory, mySite.LogFile.Directory);

            if(_installationConfiguration.LogFileCreateDirectoryWithElevatedPermissions)
            {
                if (!Directory.Exists(mySite.LogFile.Directory))
                {
                    Directory.CreateDirectory(mySite.LogFile.Directory);
                }
                else 
                { 
                    var account = new NTAccount(WindowsIdentity.GetCurrent().Name);
                    var existingDirectory = new DirectoryInfo(mySite.LogFile.Directory);
                    var existingDirectorySecurity = existingDirectory.GetAccessControl();
                    existingDirectorySecurity.SetOwner(account);
                    existingDirectory.SetAccessControl(existingDirectorySecurity);
                }       
            }
        }

        private void ConfigureAdditionalDirectories()
        {
            foreach (var directory in _installationConfiguration.AdditionalDirectories)
            {
                Directory.CreateDirectory(directory);
            }
        }

        private string NewOrOriginal(string newValue, string oldValue)
        {
            return string.IsNullOrEmpty(newValue) ? oldValue : newValue;
        }
    }
}
