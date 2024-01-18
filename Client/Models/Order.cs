using Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Order
    {
        private static Random _random = new Random();
        public int OrderNumber { get; private set; }
        public List<IPreparableItem> Basket { get; private set; }
        public bool IsCompleted { get; private set; }

        private Interface.IProvider _provider; 

        public event Action<Order> OnOrderCompleted;


        public Order(Interface.IProvider provider)
        {
            OrderNumber = _random.Next(100, 1000); 
            Basket = new List<IPreparableItem>();
            _provider = provider;
        }

        public void AddItem(IPreparableItem item)
        {
            Basket.Add(item);
        }


        public void SendOrder ()
        {
            _provider.AddOrder(this);
        }


        public void MarkAsCompleted()
        {
            IsCompleted = true;
            OnOrderCompleted?.Invoke(this);
        }

        public bool AreAllItemsCooked()
        {
            return Basket.All(item => item.IsReady);
        }

    }

}
