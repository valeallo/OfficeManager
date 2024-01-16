using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class FoodItem
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; } 
        public event Action OnCooked;

        public FoodItem(string name, int preparationTime)
        {
            Name = name;
            PreparationTime = preparationTime;
        }


        public void MarkAsCooked()
        {
            OnCooked?.Invoke();
        }
    }
}
