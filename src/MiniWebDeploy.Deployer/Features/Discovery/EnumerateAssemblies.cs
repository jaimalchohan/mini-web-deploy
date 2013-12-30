using System.Collections.Generic;
using System.IO;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class EnumerateAssemblies : IEnumerateAssemblies
    {
        public IEnumerable<string> EnumerateFrom(string path)
        {
            return Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
        }
    }
}