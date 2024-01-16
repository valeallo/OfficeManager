using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<CookingSpot> CookingSpots { get; set; }
        public List<Menu> Menus { get; set; }
        private List<Order> _allOrders;
        private Queue<Order> _orderQueue;

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
                spot.OnCookingSpotFreed += ProcessOrders; 
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

            ProcessOrders();
        }


        public void ProcessOrders()
        {
          if (_orderQueue.Count == 0)
            {
                return;
            }
          var order = _orderQueue.Peek();

          if (order.AreAllItemsCooked() || order.FoodItems.Count() == 0)
            {
                order.MarkAsCompleted();
                _orderQueue.Dequeue();
                return;
            }



            foreach (var spot in CookingSpots)
            {
                if (!spot.IsOccupied)
                {
                    var foodItem = order.FoodItems.FirstOrDefault(item => !item.IsCooked);
                    if (foodItem != null)
                    {
                        spot.CookFoodItem(foodItem); 
                    }
                }
            }
        }
    }
}
