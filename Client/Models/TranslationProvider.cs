using Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
 
    public class TranslationProvider :IProvider
    {
        public string Name { get; set; }
        private List<Translation> _translations;
        private List<Order> _allOrders;
        private Queue<Order> _orderQueue;
        private readonly object _queueLock = new object();

        public TranslationProvider(string name)
        {
            Name = name;
            _translations = new List<Translation>();
            _allOrders = new List<Order>();
            _orderQueue = new Queue<Order>();

        }

        public void AddTranslation(Translation translation)
        {
            _translations.Add(translation);
        }

        public List<Translation> getTranslations()
        {
            return _translations;
        }


        public void AddOrder(Order order)
        {
            _allOrders.Add(order);
            _orderQueue.Enqueue(order);
            ProcessOrder(order);
        }

   
        public void CheckQueueAndProcess () 
        { 
            if (_orderQueue.Count > 0)
            {
                var order = _orderQueue.Peek();
                ProcessOrder(order);
                return;
            }
            return;
        
        }



        public void ProcessOrder(Order order)

        {
            for (int i = 0; i < order.Basket.Count; i++)
            {
  
                IPreparableItem item = order.Basket[i];
                bool isLast = i == order.Basket.Count - 1;
                if (!item.IsReady && item is Translation translation)
                {
                    TimeSpan shorterDelay = TimeSpan.FromSeconds(1);
                    Task.Delay(shorterDelay).ContinueWith(_ =>
                    {
                        translation.MarkAsReady();
                
                        
                        if (order.AreAllItemsReady() && isLast)
                        {
                            order.MarkAsCompleted();
                            CheckQueueAndProcess();
                            return;
                        }
                        
                    });
                   
                }
                  
                
            }

           

        }
    }
}
