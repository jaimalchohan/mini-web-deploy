using BitDeploy;

namespace BitDeploy.TestSite
{
    public class SiteInstaller : ISiteInstaller
    {
        public void Install(IInstallationConfiguration x)
        {
            x.WithSiteName("BitDeploy")
                .AndDefaultHttpBinding()
                .AndHttpBinding("bitdeploy.com")
                .AndHttpBinding("bitdeploy.com", "127.0.0.1")
                .AndAutoStart()
                .AndDeleteExistingSite();

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