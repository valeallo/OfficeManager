using Client.enums;
using Client.Interface;
using Client.Models;
using Client.Portals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Offices
{
    internal class TranslationOffice 
    {
        public IProvider GetServices()
        {
            TranslationPortal portal = TranslationPortal.Instance;
            return portal.GetService();
        }

       

    }
}
