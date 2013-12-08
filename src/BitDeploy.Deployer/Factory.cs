namespace BitDeploy.Deployer
{
    public class Factory : IFactory
    {
        private bool _autoStart = true;

        public string SiteName { get; private set; }
        public bool AutoStart { get { return _autoStart; } }
        public string AppPoolName { get; private set; }
        public string SitePath { get; private set; }

        public Factory(string sitePath)
        {
            SitePath = sitePath;
        }

        public void SetSiteName(string siteName)
        {
            SiteName = siteName;
        }

        public void SetSitePath(string sitePath)
        {
            SitePath = sitePath;
        }

        public void SetAutoStart(bool autoStart)
        {
            _autoStart = autoStart;
        }

        public void SetAppPool(string appPoolName)
        {
            AppPoolName = appPoolName;
        }
    }
}
