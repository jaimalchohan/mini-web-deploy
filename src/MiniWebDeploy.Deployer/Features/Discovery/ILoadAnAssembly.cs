using System.Reflection;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public interface ILoadAnAssembly
    {
        Assembly Load(string path);
    }
}