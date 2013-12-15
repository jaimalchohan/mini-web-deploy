namespace BitDeploy.Deployer.Features.Discovery
{
    public class AssemblyDetails
    {
        public string Path { get; set; }
        public string BinaryPath { get; set; }

        public AssemblyDetails(string path, string binaryPath)
        {
            Path = path;
            BinaryPath = binaryPath;
        }
    }
}