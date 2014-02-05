using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Infrastructure.IIS7Plus
{
    public class ServerManagerWrapper : IServerManager
    {
        private readonly ServerManager _inner;

        public ServerManagerWrapper()
        {
            _inner = new ServerManager();
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public ApplicationPoolCollection ApplicationPools {get { return _inner.ApplicationPools; }}
        public SiteCollection Sites { get { return _inner.Sites; }}

        public void CommitChanges()
        {
            _inner.CommitChanges();
        }
    }
}