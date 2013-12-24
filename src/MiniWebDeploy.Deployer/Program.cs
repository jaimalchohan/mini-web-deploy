using System;
using System.IO;
using MiniWebDeploy.Deployer.Features.Discovery;
using MiniWebDeploy.Deployer.Features.Installation;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;

namespace MiniWebDeploy.Deployer
{
    class Program
    {
        static void Main(string[] args)
        {
            var unpackedDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            var pathScanner = new PathScanner(unpackedDirectory);
            var deploymentManifest = pathScanner.FindFirstAvailableInstaller();

            if (deploymentManifest is NoInstallationFound)
            {
                Environment.Exit((int)ExitCodes.NoInstallationPerformed);
            }

            using (var serverManager = new ServerManagerWrapper())
            {
                new SiteDeployer(serverManager, deploymentManifest.InstallationConfiguration).Deploy();
            }
        }
    }
}
