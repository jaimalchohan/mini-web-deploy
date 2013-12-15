namespace BitDeploy
{
    public interface IInstallationConfiguration
    {
        ISiteFactory WithSiteName(string siteName);
        IAppPoolFactory WithAppPool(string appPoolName);
        ILogFileFactory WithLogFile();
        void WithDirectory(string directory);
    }
}
