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
    internal class DeliveryOffice : IOffice
    {
        Restaurant _provider;

        public Restaurant GetServices()
        {
            RestaurantPortal portal = RestaurantPortal.Instance;
            _provider = portal.GetService();
            return _provider;
        }
        public void SendOrder(Order order)
        {
            _provider.AddOrder(order);
        }
    }
}
