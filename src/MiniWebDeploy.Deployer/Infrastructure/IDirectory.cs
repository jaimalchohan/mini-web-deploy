namespace MiniWebDeploy.Deployer.Infrastructure
{
    public interface IDirectory
    {
        void CreateDirectory(string directory);
        bool Exists(string directory);
    }
}
