using System.Collections.Generic;

namespace BitDeploy.Deployer.Features.Discovery
{
    public interface IDiscoverAssembliesThatHaveInstallers
    {
        List<AssemblyDetails> FindAssemblies(string path);
    }
}