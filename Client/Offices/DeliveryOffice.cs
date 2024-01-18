using Client.Interface;
using Client.Portals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Offices
{
    internal class DeliveryOffice
    {

        public IProvider GetServices()
        {
            RestaurantPortal portal = RestaurantPortal.Instance;
            return portal.GetService();
        }

    }
}
