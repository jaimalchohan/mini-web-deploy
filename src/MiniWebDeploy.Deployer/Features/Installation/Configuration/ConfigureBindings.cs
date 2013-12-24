using System.Linq;
using MiniWebDeploy.Deployer.Infrastructure.IIS7Plus;
using Microsoft.Web.Administration;

namespace MiniWebDeploy.Deployer.Features.Installation.Configuration
{
    public class ConfigureBindings : ConfigurationTaskBase
    {
        public ConfigureBindings(IServerManager serverManager)
            : base(serverManager)
        {
        }

        public override void ConfigureInstalledSite(Site site, InstallationConfiguration configuration)
        {
            if (!configuration.Bindings.Any())
            {
                return;
            }

            site.Bindings.Clear();

            foreach (var binding in configuration.Bindings)
            {
                var b = site.Bindings.CreateElement();
                b.Protocol = binding.Protocol;
                b.BindingInformation = string.Format("{0}:{1}:{2}", binding.IPAddress, binding.Port, binding.Host);
                site.Bindings.Add(b);
            }
        }
    }
}