using System.Collections.Generic;

namespace BitDeploy.Deployer.Features.Discovery
{
    public interface IFindSiteInstallers
    {
        IList<ISiteInstaller> DiscoverSiteInstallers();
    }
}