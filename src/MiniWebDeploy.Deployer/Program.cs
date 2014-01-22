using System;
using System.IO;
using System.Linq;
using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using System.Diagnostics;
using MiniWebDeploy.Deployer.Infrastructure;

namespace MiniWebDeploy.Deployer
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            var options = new ArgsParser().Parse(args);
            
            var unpackedDirectory = options.Count > 0 ? options.First().Value : Directory.GetCurrentDirectory();
            var pathScanner = new PathScanner(unpackedDirectory, options);
            var deploymentManifest = pathScanner.FindFirstAvailableInstaller();

            if (deploymentManifest is NoInstallationFound)
            {
                Environment.Exit((int)ExitCodes.NoInstallationPerformed);
            }

            deploymentManifest.SourceInstaller.ConfigureInstallation(deploymentManifest.InstallationConfiguration);

            using (var serverManager = new ServerManagerWrapper())
            {
                new SiteDeployer(serverManager, deploymentManifest.InstallationConfiguration, new DirectoryWrapper()).Deploy();
            }
        }
    }
}
