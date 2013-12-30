using System;

namespace MiniWebDeploy.Deployer.Features.Discovery
{
    public class AssemblyDetails
    {
        public string Path { get; private set; }
        public string BinaryPath { get; private set; }
        public Type InstallerType { get; private set; }

        public AssemblyDetails(string path, string binaryPath, Type installerType)
        {
            Path = path;
            BinaryPath = binaryPath;
            InstallerType = installerType;
        }
    }
}