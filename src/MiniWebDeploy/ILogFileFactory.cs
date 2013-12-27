namespace MiniWebDeploy

{
    public interface ILogFileFactory
    {
        /// <summary>
        /// Specficy the directory where IIS Logs should be saved, else use the IIS default
        /// </summary>
        /// <param name="sitePath">Path to site as an absolute path e.g. C:\inetpub\logfiles\iis</param>
        ILogFileFactory AndDirectory(string directory);
        
        /// <summary>
        /// Create the directory with the same permissions as the user installing the site, else the directory
        /// will be created with the IIS default restrictive permissions, which can cause issues with other
        /// applications e.g. log shipping)
        /// </summary>
        ILogFileFactory AndCreateDirectoryWithElevatedPermissions();
    }
}
