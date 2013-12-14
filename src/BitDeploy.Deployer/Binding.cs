namespace BitDeploy.Deployer
{
    public class Binding
    {
        private string _ipAddress = "*";
        private string _protocol = "http";
        private int? _port = null;

        public string Protocol { get { return _protocol; } }
        public string Host { get; private set; }
        public string IPAddress { get { return _ipAddress; } }

        public int Port
        {
            get
            {
                return _port.HasValue
                    ? _port.Value
                    : _protocol.Equals("http") ? 80 : 443;
            }
            private set { _port = value; }
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
