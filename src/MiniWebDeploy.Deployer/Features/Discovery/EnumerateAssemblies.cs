using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class EnumerateAssemblies : IEnumerateAssemblies
    {
        public IEnumerable<string> EnumerateFrom(string path)
        {
            return Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
        }
    }
}