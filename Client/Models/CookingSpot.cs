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
            TimeSpan randomDelay = TimeSpan.FromSeconds(randomDelayInSeconds);
           
            //Task.Delay(TimeSpan.FromMinutes(order.PreparationTime)).ContinueWith(_ =>
            //{
            //    CurrentOrder = null; 
            //    order.IsCooked = true;
            //    OnCookingSpotFreed?.Invoke(); 
            //});

            TimeSpan shorterDelay = TimeSpan.FromSeconds(1);

            Task.Delay(shorterDelay).ContinueWith(_ =>
            {
                CurrentOrder = null;
                order.MarkAsReady();
                OnCookingSpotFreed?.Invoke();
            });


        }

        //public bool CheckIfOrderIsCompleted()
        //{
        //    if (CurrentOrder == null)
        //    {
        //        OnCookingSpotFreed?.Invoke(); 
        //        return true;
        //    }

        //    var orderDuration = DateTime.Now - OrderStartTime;
        //    if (orderDuration.TotalMinutes >= CurrentOrder.PreparationTime)
        //    {
        //        CurrentOrder = null;
        //        OnCookingSpotFreed?.Invoke(); 
        //        return true;
        //    }
        //    return false;
        //}
    }
}
