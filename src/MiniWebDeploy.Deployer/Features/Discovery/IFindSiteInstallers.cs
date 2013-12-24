using System.Collections.Generic;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public interface IFindSiteInstallers
    {
        IList<ISiteInstaller> DiscoverSiteInstallers();
    }
}