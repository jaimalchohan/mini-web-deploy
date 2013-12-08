using BitDeploy;

namespace BitDeploy.TestSite
{
    public class SiteInstaller : ISiteInstaller
    {
        public void Install(IFactory x)
        {
            x.SetSiteName("BitDeploy");
            x.SetAppPool("MyAppPool");
            x.SetAutoStart(false);
        }
    }
}