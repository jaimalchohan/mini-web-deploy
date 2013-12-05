using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class SiteInstaller : ISiteInstaller
    {
        public void Install(IFactory x)
        {
            x.SetSiteName("jaimal");
            x.SetAutoStart(false);
        }
    }
}