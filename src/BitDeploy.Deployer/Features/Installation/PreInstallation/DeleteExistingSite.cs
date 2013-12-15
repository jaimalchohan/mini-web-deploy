using System;
using System.Linq;
using BitDeploy.Deployer.Infrastructure.IIS7Plus;

namespace BitDeploy.Deployer.Features.Installation.PreInstallation
{
    public class DeleteExistingSite : PreInstallationTaskBase
    {
        public DeleteExistingSite(IServerManager serverManager) 
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