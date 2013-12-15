using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BitDeploy.Deployer.Features.Discovery
{
    public class DiscoverAssembliesThatHaveInstallers
    {
        private string _path;

        public List<AssemblyDetails> FindAssemblies(string path)
        {
            _path = path;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyScanErrorHandler;

            var binaries = Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
            var binariesWithInstallersInThem = new List<AssemblyDetails>();

            foreach (var binaryPath in binaries)
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(Path.Combine(path, binaryPath));

                var singleInstanceOfASiteInstallerInAllLoadedAssemblies = assembly.GetTypes()
                    .SingleOrDefault(x => x.GetInterfaces().Select(y => y.AssemblyQualifiedName)
                        .Contains(typeof (ISiteInstaller).AssemblyQualifiedName));

                if (singleInstanceOfASiteInstallerInAllLoadedAssemblies != null)
                {
                    binariesWithInstallersInThem.Add(new AssemblyDetails(path, binaryPath));
                }
            }
            
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AssemblyScanErrorHandler;

            return binariesWithInstallersInThem;
        }

        private Assembly AssemblyScanErrorHandler(object sender, ResolveEventArgs rargs)
        {
            try
            {
                return Assembly.ReflectionOnlyLoadFrom(Path.Combine(_path, rargs.Name.Split(',')[0] + ".dll"));
            }
            catch (FileNotFoundException)
            {
                return Assembly.ReflectionOnlyLoad(rargs.Name);
            }
        }
    }
}