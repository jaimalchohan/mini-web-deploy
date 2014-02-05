using System;
using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Infrastructure.IIS7Plus
{
    public interface IServerManager : IDisposable
    {
        ApplicationPoolCollection ApplicationPools { get; }
        SiteCollection Sites { get; }

        void CommitChanges();
    }
}