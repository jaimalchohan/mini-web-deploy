using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BitDeploy.Deployer.Features.Installation;

namespace BitDeploy.Deployer.Features.Discovery
{
    public class PathScanner
    {
        public string Path { get; private set; }

        private readonly string _scanSitePath;
        private readonly IDiscoverAssembliesThatHaveInstallers _assemblyDiscoverer;

        public PathScanner(string scanSitePath)
            : this(scanSitePath, new DiscoverAssembliesThatHaveInstallers())
        {
        }

        public PathScanner(string scanSitePath, IDiscoverAssembliesThatHaveInstallers assemblyDiscoverer)
        {
            _scanSitePath = scanSitePath;
            _assemblyDiscoverer = assemblyDiscoverer;
            Path = System.IO.Path.Combine(scanSitePath, "bin");
        }

        public ConfiguredInstallationManifest FindFirstAvailableInstaller()
        {
            var assembliesWithInstallers = _assemblyDiscoverer.FindAssemblies(Path) ?? new List<AssemblyDetails>();
            var firstInstaller = assembliesWithInstallers.FirstOrDefault();
            
            if (firstInstaller == null)
            {
                return new NoInstallationFound();
            }
            
            var assemblyWithSiteInstaller = Assembly.LoadFrom(System.IO.Path.Combine(firstInstaller.Path, firstInstaller.BinaryPath));
            var siteInstaller = assemblyWithSiteInstaller.CreateInstance(assemblyWithSiteInstaller.FullName) as ISiteInstaller;
            var configuration = new InstallationConfiguration(_scanSitePath);

            return new ConfiguredInstallationManifest(configuration, siteInstaller, _scanSitePath);
        }
    }
}