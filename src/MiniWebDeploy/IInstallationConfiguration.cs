using System.Collections.Generic;

namespace MiniWebDeploy
{
    public interface IInstallationConfiguration
    {
        /// <summary>
        /// Site specfic settings
        /// </summary>
        /// <param name="siteName">The name of the website</param>
        ISiteFactory WithSiteName(string siteName);

        /// <summary>
        /// App Pool specfic settings
        /// </summary>
        /// <param name="siteName">The name of the app pool</param>
        IAppPoolFactory WithAppPool(string appPoolName);

        /// <summary>
        /// IIS Log settings
        /// </summary>
        ILogFileFactory WithLogFile();

        /// <summary>
        /// Helper to create a directory at the specfied path
        /// </summary>
        /// <param name="siteName">The full path to the directory</param>
        void WithDirectory(string directory);

        /// <summary>
        /// Key/Value pairs as passed in on the command line
        /// </summary>
        IDictionary<string, string> Args { get; }
    }
}
