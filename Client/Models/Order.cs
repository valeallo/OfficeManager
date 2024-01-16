using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Order
    {
        private static Random _random = new Random();
        public int OrderNumber { get; private set; }
        public List<FoodItem> FoodItems { get; private set; }
        public bool IsCompleted { get; private set; }

        public Order()
        {
            OrderNumber = _random.Next(100, 1000); 
            FoodItems = new List<FoodItem>();
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }



        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        public bool AreAllItemsCooked()
        {
            return FoodItems.All(item => item.IsCooked);
        }

    }

}
