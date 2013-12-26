namespace MiniWebDeploy
{
    public interface ISiteFactory
    {
        ISiteFactory AndDefaultHttpBinding();
        ISiteFactory AndHttpBinding(string host);
        ISiteFactory AndHttpBinding(string host, string ipAddress);
        ISiteFactory AndAutoStart();
        ISiteFactory AndDeleteExistingSite();
        ISiteFactory AndSitePath(string sitePath);
    }
}
