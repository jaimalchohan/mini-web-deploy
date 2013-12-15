using Microsoft.Web.Administration;

namespace BitDeploy.Deployer.Features.Installation.Installation
{
    public class CreateSite
    {
        private readonly ServerManager _serverManager;

        public CreateSite(ServerManager serverManager)
        {
            _serverManager = serverManager;
        }

        public Site Install(InstallationConfiguration configuration)
        {
            var site = _serverManager.Sites.Add(configuration.SiteName, configuration.SitePath, 80);
            site.ServerAutoStart = configuration.SiteAutoStart;
            return site;
        }
    }
}