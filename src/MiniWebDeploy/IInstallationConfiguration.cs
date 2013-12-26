﻿using System.Collections.Generic;

namespace MiniWebDeploy
{
    public interface IInstallationConfiguration
    {
        ISiteFactory WithSiteName(string siteName);
        IAppPoolFactory WithAppPool(string appPoolName);
        ILogFileFactory WithLogFile();
        void WithDirectory(string directory);
        IDictionary<string, string> Args { get; }
    }
}
