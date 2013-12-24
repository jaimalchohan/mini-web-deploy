using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Infrastructure.IIS7Plus
{
    public class ServerManagerWrapper : IServerManager
    {
        private readonly ServerManager _inner;

        public ServerManagerWrapper()
        {
            _inner = new ServerManager();
        }

        private ServerManagerWrapper(ServerManager remote)
        {
            _inner = remote;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public ApplicationDefaults ApplicationDefaults { get { return _inner.ApplicationDefaults; } }
        public ApplicationPoolDefaults ApplicationPoolDefaults {get { return _inner.ApplicationPoolDefaults; }}
        public ApplicationPoolCollection ApplicationPools {get { return _inner.ApplicationPools; }}
        public SiteDefaults SiteDefaults { get { return _inner.SiteDefaults; }}
        public SiteCollection Sites { get { return _inner.Sites; }}
        public VirtualDirectoryDefaults VirtualDirectoryDefaults {get { return _inner.VirtualDirectoryDefaults; }}
        public WorkerProcessCollection WorkerProcesses { get { return _inner.WorkerProcesses; } }

        public void CommitChanges()
        {
            _inner.CommitChanges();
        }

        public Configuration GetAdministrationConfiguration()
        {
            return _inner.GetAdministrationConfiguration();
        }

        public Configuration GetAdministrationConfiguration(WebConfigurationMap configMap, string configurationPath)
        {
            return _inner.GetAdministrationConfiguration(configMap, configurationPath);
        }

        public Configuration GetApplicationHostConfiguration()
        {
            return _inner.GetApplicationHostConfiguration();
        }

        public Configuration GetRedirectionConfiguration()
        {
            return _inner.GetRedirectionConfiguration();
        }

        public Configuration GetWebConfiguration(string siteName)
        {
            return _inner.GetWebConfiguration(siteName);
        }

        public Configuration GetWebConfiguration(string siteName, string virtualPath)
        {
            return _inner.GetWebConfiguration(siteName, virtualPath);
        }

        public Configuration GetWebConfiguration(WebConfigurationMap configMap, string configurationPath)
        {
            return _inner.GetWebConfiguration(configMap, configurationPath);
        }

        public IServerManager OpenRemote(string serverName)
        {
            return new ServerManagerWrapper(ServerManager.OpenRemote(serverName));
        }

        public void SetMetadata(string metadataType, object value)
        {
            _inner.SetMetadata(metadataType, value);
        }

        public object GetMetadata(string metadataType)
        {
            return _inner.GetMetadata(metadataType);
        }
    }
}