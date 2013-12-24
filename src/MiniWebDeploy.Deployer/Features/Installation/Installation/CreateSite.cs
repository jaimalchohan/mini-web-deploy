using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Features.Installation.Installation
{
    public class CreateSite
    {
        private readonly IServerManager _serverManager;

        public CreateSite(IServerManager serverManager)
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