using System;
using System.Linq;
using Microsoft.Web.Administration;
using System.IO;
using System.Security.Principal;

namespace BitDeploy.Deployer
{
    public class SiteDeployer
    {
        private Factory _factory;

        public SiteDeployer(Factory factory)
        {
            _factory = factory;
        }

        public void Deploy()
        {
            using (var serverManager = new ServerManager())
            {

                if(_factory.SiteDeleteExisting)
                {
                    var existingSite = serverManager.Sites.SingleOrDefault(x => x.Name.Equals(_factory.SiteName, StringComparison.InvariantCultureIgnoreCase));

                    if(existingSite != null)
                    {
                        serverManager.Sites.Remove(existingSite);
                    }
                }

                var mySite = serverManager.Sites.Add(_factory.SiteName, _factory.SitePath, 80);
                mySite.ServerAutoStart = _factory.SiteAutoStart;

                if (!string.IsNullOrEmpty(_factory.AppPoolName))
                {
                    if (_factory.AppPoolDeleteExisting)
                    {
                        var existingAppPool = serverManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(_factory.AppPoolName, StringComparison.InvariantCultureIgnoreCase));
                        serverManager.ApplicationPools.Remove(existingAppPool);
                    }

                    mySite.ApplicationDefaults.ApplicationPoolName = _factory.AppPoolName;

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
            var existingPool = serverManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(_factory.AppPoolName));

            if (existingPool == null)
            {
                var newPool = serverManager.ApplicationPools.Add(_factory.AppPoolName);
                newPool.ManagedRuntimeVersion = string.IsNullOrEmpty(_factory.AppPoolManagedRuntimeVersion) 
                    ? newPool.ManagedRuntimeVersion
                    : _factory.AppPoolManagedRuntimeVersion;
                newPool.SetAttributeValue("startMode", 1);
            }
        }

        private void ConfigureBindings(Site mySite)
        {
            if(_factory.Bindings.Any())
            {
                mySite.Bindings.Clear();
                
                foreach(var binding in _factory.Bindings)
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
            mySite.LogFile.Directory = NewOrOriginal(_factory.LogFileDirectory, mySite.LogFile.Directory);

            if(_factory.LogFileCreateDirectoryWithElevatedPermissions)
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
            foreach (var directory in _factory.AdditionalDirectories)
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
