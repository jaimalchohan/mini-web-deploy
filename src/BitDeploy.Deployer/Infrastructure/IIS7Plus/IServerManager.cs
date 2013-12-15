using System;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Infrastructure.IIS7Plus
{
    public interface IServerManager : IDisposable
    {
        ApplicationDefaults ApplicationDefaults { get; }
        ApplicationPoolDefaults ApplicationPoolDefaults { get; }
        ApplicationPoolCollection ApplicationPools { get; }

        SiteDefaults SiteDefaults { get; }
        SiteCollection Sites { get; }
        
        VirtualDirectoryDefaults VirtualDirectoryDefaults { get; }
        WorkerProcessCollection WorkerProcesses { get; }

        void CommitChanges();

        Configuration GetAdministrationConfiguration();
        Configuration GetAdministrationConfiguration(WebConfigurationMap configMap, string configurationPath);
        Configuration GetApplicationHostConfiguration();
        Configuration GetRedirectionConfiguration();
        Configuration GetWebConfiguration(string siteName);
        Configuration GetWebConfiguration(string siteName, string virtualPath);
        Configuration GetWebConfiguration(WebConfigurationMap configMap, string configurationPath);

        IServerManager OpenRemote(string serverName);

        void SetMetadata(string metadataType, object value);
        object GetMetadata(string metadataType);
    }
}
