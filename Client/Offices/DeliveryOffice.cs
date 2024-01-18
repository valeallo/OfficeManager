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
    internal class DeliveryOffice
    {
        Order _order;
        Restaurant _restaurant;

        public Restaurant GetServices()
        {
            RestaurantPortal portal = RestaurantPortal.Instance;
            _restaurant = portal.GetService();
            return portal.GetService();
        }

        public void addOrder (Order order)
        {
            _order = order;
        }



    }
}
