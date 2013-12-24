using System;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class AssemblyDetails
    {
        public string Path { get; set; }
        public string BinaryPath { get; set; }
        public Type InstallerType { get; set; }

        public AssemblyDetails(string path, string binaryPath, Type installerType)
        {
            Path = path;
            BinaryPath = binaryPath;
            InstallerType = installerType;
        }
    }
}