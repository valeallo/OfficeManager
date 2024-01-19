using Client.Interface;
using Client.Offices;
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

        private IOffice _office; 



        public Order(IOffice office)
        {
            OrderNumber = _random.Next(100, 1000); 
            Basket = new List<IPreparableItem>();
            _office = office;
            _office.addOrder(this);
            
        }

        public void AddItem(IPreparableItem item)
        {
            Basket.Add(item);
        }

        public bool AreAllItemsCooked()
        {
            return Basket.All(item => item.IsReady);
        }

        public void ChangeToCompleted ()
        {
            _office.MarkAsCompleted();
        }

    }

}
