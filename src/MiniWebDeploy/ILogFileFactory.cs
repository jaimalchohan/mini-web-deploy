namespace MiniWebDeploy
{
    public interface ILogFileFactory
    {
        ILogFileFactory AndDirectory(string directory);
        ILogFileFactory AndCreateDirectoryWithElevatedPermissions();
    }
}
