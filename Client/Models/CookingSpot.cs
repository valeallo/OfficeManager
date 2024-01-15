using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class CookingSpot
    {
        public FoodItem CurrentOrder { get; private set; }
        public bool IsOccupied => CurrentOrder != null;
        public DateTime OrderStartTime { get; private set; }

        public void AssignOrder(FoodItem order)
        {
            CurrentOrder = order;
            OrderStartTime = DateTime.Now;
        }

        public bool CheckIfOrderIsCompleted()
        {
            if (CurrentOrder == null) return false;

            var orderDuration = DateTime.Now - OrderStartTime;
            if (orderDuration.TotalMinutes >= CurrentOrder.PreparationTime)
            {
                CurrentOrder = null;
                return true;
            }
            return false;
        }
    }
}
