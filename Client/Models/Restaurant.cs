using Client.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Restaurant : IProvider
    {
        public string Name { get; set; }
        public List<CookingSpot> CookingSpots { get; set; }
        public List<Menu> Menus { get; set; }
        private List<Order> _allOrders;
        private Queue<Order> _orderQueue;
        private readonly object _queueLock = new object();

        public Restaurant(string name)
        {
            Name = name;
            CookingSpots = new List<CookingSpot>
        {
            new CookingSpot(),
            new CookingSpot(),
            new CookingSpot(),
            new CookingSpot()
        };
            Menus = new List<Menu>();
            _allOrders = new List<Order>();
            _orderQueue = new Queue<Order>();



            foreach (var spot in CookingSpots)
            {
                spot.OnCookingSpotFreed += CheckQueueAndProcess; 
            }
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
        }

        public int AvailableSpots()
        {
            return CookingSpots.Count(spot => !spot.IsOccupied);
        }

        public void AddOrder(Order order)
        {
            _allOrders.Add(order);
            _orderQueue.Enqueue(order);
            ProcessOrder(order);
        }

        public Menu getCurrentMenu ()
        {
            int currentHour = DateTime.Now.Hour;
            string menuType;

            if (currentHour >= 6 && currentHour < 12)
            {
                menuType = "breakfast";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                menuType = "lunch";
            }
            else
            {
                menuType = "dinner";
            }
            return Menus.FirstOrDefault(m => m.Name.Equals(menuType, StringComparison.OrdinalIgnoreCase));
        }


        public void CheckQueueAndProcess()
        {
            lock (_queueLock)
            {
                if (_orderQueue.Count > 0)
            {
                var order = _orderQueue.Peek(); 
                if (order.AreAllItemsCooked())
                {
                    order.MarkAsCompleted();
                    order.MarkAllItemsUncooked();
                    _orderQueue.Dequeue();
                }
                if (_orderQueue.Count > 0)
                {
                    ProcessOrder(_orderQueue.Peek());
                }
            }
           }
        }


        public void ProcessOrder(Order order)
        
        {
            foreach (var item in order.Basket)
            {
                foreach (var spot in CookingSpots)
                {
                    if (!spot.IsOccupied)
                    {
                        var foodItem = order.Basket.FirstOrDefault(item => !item.IsReady);
                        if (foodItem is FoodItem food)
                        {
                            spot.CookFoodItem(food);
                            break;
                        }
                    }
                }
            }

        }
    }
}
