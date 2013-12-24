namespace MiniWebDeploy
{
    public interface IAppPoolFactory
    {
        IAppPoolFactory AndManagedRuntimeVersion(string version);
        IAppPoolFactory AndDeleteExistingAppPool();
        IAppPoolFactory AndStartOnDemand();
    }
}
