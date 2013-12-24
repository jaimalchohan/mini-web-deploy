using System.Collections.Generic;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public interface IDiscoverAssembliesThatHaveInstallers
    {
        List<AssemblyDetails> FindAssemblies(string path);
    }
}