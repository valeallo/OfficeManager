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
    public class DeliveryOffice : IOffice
    {
        Order _order;
        Restaurant _provider;
        public bool IsOrderCompleted { get; private set; }

        public event Action<Order> OnOrderFinished;

        public Restaurant GetServices()
        {
            RestaurantPortal portal = RestaurantPortal.Instance;
            _provider = portal.GetService();
            return portal.GetService();
        }

        public void addOrder (Order order)
        {
            _order = order;
        }

        public void SendOrder()
        {
            _provider.AddOrder(_order);
        }

        public void MarkAsCompleted()
        {
            IsOrderCompleted = true;
            OnOrderFinished?.Invoke(_order);
        }
    }
}
