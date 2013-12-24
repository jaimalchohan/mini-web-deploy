using System.Reflection;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class LoadAnAssembly : ILoadAnAssembly
    {
        public Assembly Load(string path)
        {
            return Assembly.LoadFrom(path);
        }
    }
}