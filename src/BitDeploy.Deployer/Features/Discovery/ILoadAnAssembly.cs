using System.Reflection;

namespace BitDeploy.Deployer.Features.Discovery
{
    public interface ILoadAnAssembly
    {
        Assembly Load(string path);
    }
}