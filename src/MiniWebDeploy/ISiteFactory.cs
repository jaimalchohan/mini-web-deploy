namespace MiniWebDeploy
{
    public interface ISiteFactory
    {
        /// <summary>
        /// Create a port 80 http binding for no specfic hostname and no specfic IP address
        /// </summary>
        ISiteFactory AndDefaultHttpBinding();

        /// <summary>
        /// Create a port 80 http binding for the specfied hostname and no specfic IP address
        /// </summary>
        /// <param name="host">Hostname e.g. www.example.com</param>
        ISiteFactory AndHttpBinding(string host);

        /// <summary>
        /// Create a port 80 http binding for the specfied hostname and IP address
        /// </summary>
        /// <param name="host">Hostname e.g. www.example.com</param>
        /// <param name="ipAddress">IPv4 address e.g. 192.168.0.1</param>
        ISiteFactory AndHttpBinding(string host, string ipAddress);

        /// <summary>
        /// Starts the webiste on creation
        /// </summary>
        ISiteFactory AndAutoStart();

        /// <summary>
        /// Delete an eistsing site with the same site name if it exists.
        /// </summary>
        ISiteFactory AndDeleteExistingSite();

        /// <summary>
        /// Set the physical path for the site, else the path specifed at the command line will be used
        /// </summary>
        /// <param name="sitePath">Path to site as an absolute path e.g. C:\inetpub\wwwroot\default</param>
        ISiteFactory AndSitePath(string sitePath);
    }
}
