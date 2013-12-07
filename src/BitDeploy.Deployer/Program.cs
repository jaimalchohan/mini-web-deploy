using ClassLibrary1;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            path = Path.Combine(path, "bin");

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
                    f = new Factory();
                    instance.GetType().InvokeMember("Install", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, instance, new[] { f });
                    break;
                }
            }

            if(f != null)
            {
                DoInstall(f, args[0]);
            }
        }

        public static void DoInstall(Factory f, string path)
        {
            var serverManager = new ServerManager();
            var mySite = serverManager.Sites.Add(f.SiteName, path, 80);
            mySite.ServerAutoStart = f.AutoStart;
            serverManager.CommitChanges();

        }
    }
}
