using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Client.Models.Food;

namespace Client.Models
{
    public class Order
    {
        private static Random _random = new Random();
        public int OrderNumber { get; private set; }
        public List<FoodItem> FoodItems { get; private set; }
        public bool IsCompleted { get; private set; }

        private Restaurant _restaurant; 

        public event Action<Order> OnOrderCompleted;


        public Order(Restaurant restaurant)
        {
            OrderNumber = _random.Next(100, 1000); 
            FoodItems = new List<FoodItem>();
            _restaurant = restaurant;
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }


        public void SendOrder ()
        {
            _restaurant.AddOrder(this);
        }


        public void MarkAsCompleted()
        {
            IsCompleted = true;
            OnOrderCompleted?.Invoke(this);
        }

        public bool AreAllItemsCooked()
        {
            return FoodItems.All(item => item.IsCooked);
        }

    }

}
