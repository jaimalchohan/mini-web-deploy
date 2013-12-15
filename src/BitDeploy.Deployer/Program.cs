using System;
using System.IO;
using BitDeploy.Deployer.Features.Discovery;
using BitDeploy.Deployer.Features.Installation;
using BitDeploy.Deployer.Infrastructure.IIS7Plus;

namespace BitDeploy.Deployer
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
