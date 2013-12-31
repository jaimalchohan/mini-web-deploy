using System.Collections.Generic;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public interface IEnumerateAssemblies
    {
        IEnumerable<string> EnumerateFrom(string path);
    }
}