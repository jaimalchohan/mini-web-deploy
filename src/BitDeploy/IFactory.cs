namespace BitDeploy
{
    public interface IFactory
    {
        void SetSiteName(string siteName);
        void SetAutoStart(bool autoStart);
        void SetAppPool(string appPoolName);
    }
}
