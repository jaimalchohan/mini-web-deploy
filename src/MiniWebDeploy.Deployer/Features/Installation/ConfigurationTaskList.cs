using System.Collections.Generic;
using MiniWebDeploy.Deployer.Features.Installation.Configuration;
using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Features.Installation
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