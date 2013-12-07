using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Factory : IFactory
    {
        private bool _autoStart = true;

        public string SiteName { get; private set; }
        public bool AutoStart { get { return _autoStart; } }

        public void SetSiteName(string siteName)
        {
            SiteName = siteName;
        }

        public void SetAutoStart(bool autoStart)
        {
            _autoStart = autoStart;
        }
    }
}
