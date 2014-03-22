namespace MiniWebDeploy.Deployer.Features.Installation.Installation
{
    public class Binding
    {
        private readonly string _ipAddress = "*";
        private bool _sslEnabled = false;

        public string Protocol 
        { 
            get 
            {
                if (_sslEnabled)
                    return "https";
                else
                    return "http";
            } 
        }

        public string Host { get; private set; }
        public string IPAddress { get { return _ipAddress; } }

        public int Port
        {
            get
            {
                if (_sslEnabled)
                    return 443;
                else
                    return 80;
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

        public Binding(string host, string ipAddress, bool ssl)
            : this(host, ipAddress)
        {
            _sslEnabled = ssl;
        }

        public Binding(string host, bool ssl)
            : this(host)
        {
            _sslEnabled = ssl;
        }
    }
}
