using System.Reflection;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class LoadAnAssembly : ILoadAnAssembly
    {
        public Assembly LoadFrom(string path)
        {
            return Assembly.LoadFrom(path);
        }

        public Assembly ReflectionOnlyLoadFrom(string path)
        {
            return Assembly.ReflectionOnlyLoadFrom(path);
        }

        public Assembly ReflectionOnlyLoad(string assemblyName)
        {
            return Assembly.ReflectionOnlyLoad(assemblyName);
        }
    }
}