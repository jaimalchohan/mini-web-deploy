using System.Collections.Generic;

namespace BitDeploy.Deployer.Features.Installation
{
    public class InstallationConfiguration : IInstallationConfiguration, ISiteFactory, IAppPoolFactory, ILogFileFactory
    {
        private string _startMode = "AlwaysRunning";

        public string SiteName { get; private set; }
        public bool SiteAutoStart { get; private set; }
        public string SitePath { get; private set; }
        public bool SiteDeleteExisting { get; private set; }
        public IList<Binding> Bindings { get; private set; }
        
        public string AppPoolName { get; private set; }
        public string AppPoolManagedRuntimeVersion { get; private set; }
        public bool AppPoolDeleteExisting { get; private set; }
        public string AppPoolStartMode { get { return _startMode; } }
        
        public string LogFileDirectory { get; private set; }
        public bool LogFileCreateDirectoryWithElevatedPermissions { get; private set; }

        public List<string> AdditionalDirectories { get; private set; }

        public InstallationConfiguration(string sitePath)
        {
            SitePath = sitePath;
            Bindings = new List<Binding>();
            AdditionalDirectories = new List<string>();
        }

        public ISiteFactory WithSiteName(string siteName)
        {
            SiteName = siteName;
            return this;
        }

        public void SetSitePath(string sitePath)
        {
            SitePath = sitePath;
        }

        public ISiteFactory AndAutoStart()
        {
            SiteAutoStart = true;
            return this;
        }

        public IAppPoolFactory WithAppPool(string appPoolName)
        {
            AppPoolName = appPoolName;
            return this;
        }

        public ISiteFactory AndDefaultHttpBinding()
        {
            Bindings.Add(new Binding());
            return this;
        }

        public ISiteFactory AndHttpBinding(string host)
        {
            Bindings.Add(new Binding(host));
            return this;
        }

        public ISiteFactory AndHttpBinding(string host, string ipAddress)
        {
            Bindings.Add(new Binding(host, ipAddress));
            return this;
        }

        public ISiteFactory AndDeleteExistingSite()
        {
            SiteDeleteExisting = true;
            return this;
        }

        public IAppPoolFactory AndManagedRuntimeVersion(string version)
        {
            AppPoolManagedRuntimeVersion = version;
            return this;
        }


        public IAppPoolFactory AndDeleteExistingAppPool()
        {
            AppPoolDeleteExisting = true;
            return this;
        }

        public IAppPoolFactory AndStartOnDemand()
        {
            _startMode = "OnDemand";
            return this;
        }

        public ILogFileFactory WithLogFile()
        {
            return this;
        }

        public ILogFileFactory AndDirectory(string directory)
        {
            LogFileDirectory = directory;
            return this;
        }

        public ILogFileFactory AndCreateDirectoryWithElevatedPermissions()
        {
            LogFileCreateDirectoryWithElevatedPermissions = true;
            return this;
        }

        public void WithDirectory(string directory)
        {
            AdditionalDirectories.Add(directory);
        }
    }
}
