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
        public event Action OnReady;
        public bool IsReady { get; set; }

        public FoodItem(string name, int preparationTime)
        {
            Name = name;
            PreparationTime = preparationTime;
            IsReady = false;
        }


        public void MarkAsReady()
        {
            IsReady = true;
            OnReady?.Invoke();
        }
    }
}
