namespace Client.Models
{
    public class CookingSpot
    {
        public FoodItem CurrentOrder { get; private set; }
        public bool IsOccupied => CurrentOrder != null;
        public DateTime OrderStartTime { get; private set; }
        public event Action OnCookingSpotFreed;

        public void CookFoodItem(FoodItem order)
        {
            CurrentOrder = order;
            OrderStartTime = DateTime.Now;
            Task.Delay(TimeSpan.FromMinutes(order.PreparationTime)).ContinueWith(_ =>
            {
                CurrentOrder = null; 
                order.IsCooked = true;
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
