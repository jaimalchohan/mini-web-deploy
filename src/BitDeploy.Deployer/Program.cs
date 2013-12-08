using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BitDeploy.Deployer
{
    class Program
    {
        static void Main(string[] args)
        {
            var scanSitePath = args[0];
            var path = Path.Combine(scanSitePath, "bin");

            var binaries = Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly);

            Factory f = null;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += delegate(object sender, ResolveEventArgs rargs)
            {
                try
                {
                    return Assembly.ReflectionOnlyLoadFrom(Path.Combine(path, rargs.Name.Split(',')[0] + ".dll"));
                }
                catch (FileNotFoundException)
                {
                    return Assembly.ReflectionOnlyLoad(rargs.Name);
                }
            };

            foreach (var binaryPath in binaries)
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(Path.Combine(path, binaryPath));
                
                var t = assembly.GetTypes().SingleOrDefault(x => x.GetInterfaces().Select(y => y.AssemblyQualifiedName).Contains(typeof(ISiteInstaller).AssemblyQualifiedName));

                if(t != null)
                {
                    var ass = Assembly.LoadFrom(Path.Combine(path, binaryPath));
                    var instance = ass.CreateInstance(t.FullName);
                    f = new Factory(scanSitePath);
                    instance.GetType().InvokeMember("Install", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, instance, new[] { f });
                    break;
                }
            }

            if(f != null)
            {
                var deployer = new SiteDeployer(f);
                deployer.Deploy();
            }
        }
    }
}
