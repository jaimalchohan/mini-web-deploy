using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class DiscoverAssembliesThatHaveInstallers : IDiscoverAssembliesThatHaveInstallers
    {
        private string _path;
        private readonly ILoadAnAssembly _assemblyLoader;
        private readonly IEnumerateAssemblies _assemblyEnumerator;

        public DiscoverAssembliesThatHaveInstallers(ILoadAnAssembly assemblyLoader, IEnumerateAssemblies assemblyEnumerator)
        {
            _assemblyLoader = assemblyLoader;
            _assemblyEnumerator = assemblyEnumerator;
        }

        public List<AssemblyDetails> FindAssemblies(string path)
        {
            Trace.TraceInformation("Searching for assemblies with ISiteInstaller implementations at " + path);

            _path = path;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyScanErrorHandler;

            var binaries = _assemblyEnumerator.EnumerateFrom(path);
            var binariesWithInstallersInThem = new List<AssemblyDetails>();

            foreach (var binaryPath in binaries)
            {
                var assembly = _assemblyLoader.ReflectionOnlyLoadFrom(binaryPath);

                var singleInstanceOfASiteInstallerInAllLoadedAssemblies = assembly.GetTypes()
                    .SingleOrDefault(x => x.GetInterfaces().Select(y => y.AssemblyQualifiedName)
                        .Contains(typeof (ISiteInstaller).AssemblyQualifiedName));

                if (singleInstanceOfASiteInstallerInAllLoadedAssemblies != null)
                {
                    Trace.TraceInformation("Found assembly with ISiteInstaller implementations at " + binaryPath);
                    binariesWithInstallersInThem.Add(new AssemblyDetails(path, binaryPath, singleInstanceOfASiteInstallerInAllLoadedAssemblies));
                }
            }

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AssemblyScanErrorHandler;

            return binariesWithInstallersInThem;
        }

        private Assembly AssemblyScanErrorHandler(object sender, ResolveEventArgs rargs)
        {
            try
            {
                return _assemblyLoader.ReflectionOnlyLoadFrom(Path.Combine(_path, rargs.Name.Split(',')[0] + ".dll"));
            }
            catch (FileNotFoundException)
            {
                return _assemblyLoader.ReflectionOnlyLoad(rargs.Name);
            }
        }
    }
}