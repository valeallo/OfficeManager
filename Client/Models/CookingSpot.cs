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
            Random random = new Random();
            int randomDelayInSeconds = random.Next(0, 21);
            TimeSpan prepTime = TimeSpan.FromMinutes(order.PreparationTime);
            //TimeSpan shorterDelay = TimeSpan.FromSeconds(5);

            Task.Delay(prepTime).ContinueWith(_ =>
            {
                CurrentOrder = null;
                order.MarkAsReady();
                OnCookingSpotFreed?.Invoke();
            });


        }

       
    }
}
