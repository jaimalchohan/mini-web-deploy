﻿using System.Collections.Generic;
using System.Linq;
using BitDeploy.Deployer.Features.Installation;

namespace BitDeploy.Deployer.Features.Discovery
{
    public class PathScanner
    {
        public string Path { get; private set; }

        private readonly string _scanSitePath;
        private readonly IDiscoverAssembliesThatHaveInstallers _assemblyDiscoverer;
        private readonly ILoadAnAssembly _assemblyLoader;

        public PathScanner(string scanSitePath)
            : this(scanSitePath, new DiscoverAssembliesThatHaveInstallers(), new LoadAnAssembly())
        {
        }

        public PathScanner(string scanSitePath, IDiscoverAssembliesThatHaveInstallers assemblyDiscoverer, ILoadAnAssembly assemblyLoader)
        {
            _scanSitePath = scanSitePath;
            _assemblyDiscoverer = assemblyDiscoverer;
            _assemblyLoader = assemblyLoader;
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
            
            var assemblyWithSiteInstaller = _assemblyLoader.Load(System.IO.Path.Combine(firstInstaller.Path, firstInstaller.BinaryPath));
            var siteInstaller = assemblyWithSiteInstaller.CreateInstance(firstInstaller.InstallerType.FullName, true);
            var configuration = new InstallationConfiguration(_scanSitePath);

            return new ConfiguredInstallationManifest(configuration, (ISiteInstaller)siteInstaller, _scanSitePath, firstInstaller);
        }
    }
}