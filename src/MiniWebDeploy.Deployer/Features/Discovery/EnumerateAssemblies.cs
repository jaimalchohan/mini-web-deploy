using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    [ExcludeFromCodeCoverage]
    public class EnumerateAssemblies : IEnumerateAssemblies
    {
        public IEnumerable<string> EnumerateFrom(string path)
        {
            return Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
        }
    }
}