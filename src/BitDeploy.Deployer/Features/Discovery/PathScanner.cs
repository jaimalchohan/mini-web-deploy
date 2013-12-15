using System.IO;
using System.Linq;
using System.Reflection;
using BitDeploy.Deployer.Features.Installation;

namespace BitDeploy.Deployer.Features.Discovery
{
    public class PathScanner
    {
        private readonly string _scanSitePath;
        private readonly string _path;
        private readonly DiscoverAssembliesThatHaveInstallers _assemblyDiscoverer;

        public PathScanner(string scanSitePath)
            : this(scanSitePath, new DiscoverAssembliesThatHaveInstallers())
        {
        }

        public PathScanner(string scanSitePath, DiscoverAssembliesThatHaveInstallers assemblyDiscoverer)
        {
            _scanSitePath = scanSitePath;
            _assemblyDiscoverer = assemblyDiscoverer;
            _path = Path.Combine(scanSitePath, "bin");
        }

        public ConfiguredInstallationManifest FindFirstAvailableInstaller()
        {
            var assembliesWithInstallers = _assemblyDiscoverer.FindAssemblies(_path);
            var firstInstaller = assembliesWithInstallers.FirstOrDefault();
            
            if (firstInstaller == null)
            {
                return new NoInstallationFound();
            }
            
            var assemblyWithSiteInstaller = Assembly.LoadFrom(Path.Combine(firstInstaller.Path, firstInstaller.BinaryPath));
            var siteInstaller = assemblyWithSiteInstaller.CreateInstance(assemblyWithSiteInstaller.FullName) as ISiteInstaller;
            var configuration = new InstallationConfiguration(_scanSitePath);

            return new ConfiguredInstallationManifest(configuration, siteInstaller, _scanSitePath);
        }
    }
}