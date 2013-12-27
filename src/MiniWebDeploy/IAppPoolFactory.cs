namespace MiniWebDeploy
{
    public interface IAppPoolFactory
    {
        /// <summary>
        /// The runtime version for the app pool.  Valid versions are "v1.1", "v2.0" and "v4.0"
        /// </summary>
        IAppPoolFactory AndManagedRuntimeVersion(string version);

        /// <summary>
        /// Delete the existsing app pool if an app pool with the same name already exists
        /// </summary>
        IAppPoolFactory AndDeleteExistingAppPool();

        /// <summary>
        /// If specified, starts the App Pool OnDemand rather than have it AlwaysRunning
        /// </summary>
        IAppPoolFactory AndStartOnDemand();
    }
}
