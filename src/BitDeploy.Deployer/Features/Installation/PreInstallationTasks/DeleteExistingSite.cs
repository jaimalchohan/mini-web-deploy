using System;
using System.Linq;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.PreInstallationTasks
{
    public class DeleteExistingSite : PreInstallationTaskBase
    {
        public DeleteExistingSite(ServerManager serverManager) 
            : base(serverManager)
        {
        }

        public override void BeforeInstallation(InstallationConfiguration configuration)
        {
            if (!configuration.SiteDeleteExisting)
            {
                return;
            }
            
            var existingSite = ServerManager.Sites.SingleOrDefault(x =>x.Name.Equals(configuration.SiteName, StringComparison.InvariantCultureIgnoreCase));

            if (existingSite == null)
            {
                return;
            }

            ServerManager.Sites.Remove(existingSite);
        }
    }
}