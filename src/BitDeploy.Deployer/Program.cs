using System;
using BitDeploy.Deployer.Features.Discovery;
using BitDeploy.Deployer.Features.Installation;

namespace BitDeploy.Deployer
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathScanner = new PathScanner(args[0]);
            var deploymentManifest = pathScanner.FindFirstAvailableInstaller();

            if (deploymentManifest is NoInstallationFound)
            {
                Environment.Exit((int)ExitCodes.NoInstallationPerformed);
            }

            new SiteDeployer(deploymentManifest.InstallationConfiguration).Deploy();
        }
    }
}
