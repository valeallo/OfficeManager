using Client.enums;
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
        public OrderStatus Status { get; set; }

        private IOffice _office; 

        public event Action<Order> OnOrderCompleted;


        public Order(IOffice office)
        {
            OrderNumber = _random.Next(100, 1000); 
            Basket = new List<IPreparableItem>();
            _office = office;
        }

        public void AddItem(IPreparableItem item)
        {
            Basket.Add(item);
        }

       
        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            OnOrderCompleted?.Invoke(this);
        }

        public bool AreAllItemsReady()
        {
            return Basket.All(item => item.IsReady);
        }

      

    }

}
