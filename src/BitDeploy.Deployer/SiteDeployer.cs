using System;
using System.Linq;
using Microsoft.Web.Administration;

namespace BitDeploy.Deployer
{
    public class SiteDeployer
    {
        private Factory _factory;

        public SiteDeployer(Factory factory)
        {
            _factory = factory;
        }

        public void Deploy()
        {
            using (var serverManager = new ServerManager())
            {
                var mySite = serverManager.Sites.Add(_factory.SiteName, _factory.SitePath, 80);

                if (!string.IsNullOrEmpty(_factory.AppPoolName))
                {
                    mySite.ApplicationDefaults.ApplicationPoolName = _factory.AppPoolName;

                    ConfigureAppPoolIfNotExists(serverManager);
                }

                mySite.ServerAutoStart = _factory.AutoStart;
    
                serverManager.CommitChanges();
            }
        }

        public void ConfigureAppPoolIfNotExists(ServerManager serverManager)
        {
            var existingPool = serverManager.ApplicationPools.SingleOrDefault(x => x.Name.Equals(_factory.AppPoolName));

            if (existingPool == null)
            {
                var newPool = serverManager.ApplicationPools.Add(_factory.AppPoolName);
            }
        }
    }
}
