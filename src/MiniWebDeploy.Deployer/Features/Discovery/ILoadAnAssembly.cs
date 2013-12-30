using System.Reflection;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public interface ILoadAnAssembly
    {
        Assembly LoadFrom(string path);
        Assembly ReflectionOnlyLoadFrom(string path);
        Assembly ReflectionOnlyLoad(string assemblyName);
    }
}