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
        public event Action OnCookingSpotFreed;

        public void AssignOrder(FoodItem order)
        {
            CurrentOrder = order;
            OrderStartTime = DateTime.Now;
            Task.Delay(TimeSpan.FromMinutes(order.PreparationTime)).ContinueWith(_ =>
            {
                CurrentOrder = null; 
                OnCookingSpotFreed?.Invoke(); 
            });
        }

        public bool CheckIfOrderIsCompleted()
        {
            if (CurrentOrder == null)
            {
                OnCookingSpotFreed?.Invoke(); 
                return true;
            }

            var orderDuration = DateTime.Now - OrderStartTime;
            if (orderDuration.TotalMinutes >= CurrentOrder.PreparationTime)
            {
                CurrentOrder = null;
                OnCookingSpotFreed?.Invoke(); 
                return true;
            }
            return false;
        }
    }
}
