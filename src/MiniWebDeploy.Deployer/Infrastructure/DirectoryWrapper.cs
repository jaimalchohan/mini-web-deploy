using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Principal;

namespace MiniWebDeploy.Deployer.Infrastructure
{
    public class DirectoryWrapper : IDirectory
    {
        public void CreateDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public bool Exists(string directory)
        {
            return Directory.Exists(directory);
        }

        public void ElevatePermissions(string directory)
        {
            var account = new NTAccount(WindowsIdentity.GetCurrent().Name);
            var existingDirectory = new DirectoryInfo(directory);
            var existingDirectorySecurity = existingDirectory.GetAccessControl();
            existingDirectorySecurity.SetOwner(account);
            existingDirectory.SetAccessControl(existingDirectorySecurity);
        }
    }
}
