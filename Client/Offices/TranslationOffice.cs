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
    public class TranslationOffice : IOffice
    {

        Order _order;
        TranslationProvider _provider;
        public bool IsOrderCompleted { get; private set; }

        public event Action<Order> OnOrderCompleted;

        public TranslationProvider GetServices()
        {
            TranslationPortal portal = TranslationPortal.Instance;
            _provider = portal.GetService();
            return _provider;

        }


        public void addOrder(Order order)
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
            OnOrderCompleted?.Invoke(_order);
        }

    }
}
