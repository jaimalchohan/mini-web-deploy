namespace BitDeploy
{
    public interface IFactory
    {
        ISiteFactory WithSiteName(string siteName);
        IAppPoolFactory WithAppPool(string appPoolName);
        ILogFileFactory WithLogFile();
        void WithDirectory(string directory);
    }
}
