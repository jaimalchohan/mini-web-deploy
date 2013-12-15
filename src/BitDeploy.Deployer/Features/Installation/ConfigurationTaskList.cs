using System.Collections.Generic;
using BitDeploy.Deployer.Features.Installation.Configuration;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation
{
    public class ConfigurationTaskList : List<IConfigurationTask>
    {
        public void Configure(Site site, InstallationConfiguration configuration)
        {
            foreach (var task in this)
            {
                task.ConfigureInstalledSite(site, configuration);
            }
        }
    }
}