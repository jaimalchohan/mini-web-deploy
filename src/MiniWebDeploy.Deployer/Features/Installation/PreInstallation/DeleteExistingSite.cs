using System;
using System.Linq;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;

namespace MiniWebDeploy.Deployer.Features.Installation.PreInstallation
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