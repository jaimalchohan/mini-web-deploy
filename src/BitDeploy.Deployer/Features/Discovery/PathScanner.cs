using System;
using System.Collections.Generic;
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

        public PathScanner(string scanSitePath)
        {
            _scanSitePath = scanSitePath;
            _path = Path.Combine(scanSitePath, "bin");
        }

        public IList<ConfiguredInstallationManifest> DiscoverManifests()
        {
            var installers = new List<ConfiguredInstallationManifest>();

            var binaries = Directory.EnumerateFiles(_path, "*.dll", SearchOption.TopDirectoryOnly);
            
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (sender, rargs) =>
            {
                try
                {
                    return Assembly.ReflectionOnlyLoadFrom(Path.Combine(_path, rargs.Name.Split(',')[0] + ".dll"));
                }
                catch (FileNotFoundException)
                {
                    return Assembly.ReflectionOnlyLoad(rargs.Name);
                }
            };

            foreach (var binaryPath in binaries)
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(Path.Combine(_path, binaryPath));
                
                var singleInstanceOfASiteInstallerInAllLoadedAssemblies =
                    assembly.GetTypes().SingleOrDefault(x =>x.GetInterfaces()
                                    .Select(y => y.AssemblyQualifiedName)
                                    .Contains(typeof (ISiteInstaller).AssemblyQualifiedName));

                if (singleInstanceOfASiteInstallerInAllLoadedAssemblies == null)
                {
                    continue;
                }

                var assemblyWithSiteInstaller = Assembly.LoadFrom(Path.Combine(_path, binaryPath));
                var siteInstaller = assemblyWithSiteInstaller.CreateInstance(singleInstanceOfASiteInstallerInAllLoadedAssemblies.FullName) as ISiteInstaller;
                var configuration = new InstallationConfiguration(_scanSitePath);
                var configuredManifest = new ConfiguredInstallationManifest(configuration, siteInstaller, _scanSitePath);

                installers.Add(configuredManifest);
                break;
            }

            return new List<ConfiguredInstallationManifest>();
        }
    }
}