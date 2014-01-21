using MiniWebDeploy;

namespace MiniWebDeploy.TestSite
{
    public class SiteInstaller : ISiteInstaller
    {
        public void Install(IInstallationConfiguration x)
        {
            var siteName = "MiniWebDeployIntegrationSite";

            x.WithSiteName(siteName)
                .AndDefaultHttpBinding()
                .AndHttpBinding(siteName + ".com")
                .AndHttpBinding(siteName + ".com", "127.0.0.1")
                .AndAutoStart()
                .AndDeleteExistingSite()
                .AndSitePath("..\\src\\MiniWebDeploy.TestSite\\bin");

            x.WithAppPool("MyAppPool")
                .AndDeleteExistingAppPool()
                .AndManagedRuntimeVersion("v4.0")
                .AndStartOnDemand();

            x.WithLogFile()
                .AndCreateDirectoryWithElevatedPermissions();

            x.WithDirectory("c:\\Logs");

        }
    }
}