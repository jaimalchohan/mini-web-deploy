namespace MiniWebDeploy.Deployer.Features.Installation.Installation
{
    public class Binding
    {
        private readonly string _ipAddress = "*";
        private const string _protocol = "http";
        
        public string Protocol { get { return _protocol; } }
        public string Host { get; private set; }
        public string IPAddress { get { return _ipAddress; } }

        public int Port
        {
            get
            {
                return _protocol.Equals("http") ? 80 : 443;
            }
        }

        public Binding()
        {

        }

        public Binding(string host)
        {
            Host = host;
        }

        public Binding(string host, string ipAddress)
        {
            Host = host;
            _ipAddress = ipAddress;
        }
    }
}
