using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interface
{
    public interface IProvider
    {
        public void AddOrder(Order order);
        public void ProcessOrders();
    }
}
