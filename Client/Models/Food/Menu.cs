using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models.Food;

namespace Client.Models
{
    public class Menu
    {
        public string Name { get; set; }
        public List<FoodItem> FoodItems { get; set; }

        public Menu(string name)
        {
            Name = name;
            FoodItems = new List<FoodItem>();
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }
    }

}
