using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interface
{
    public interface IPreparableItem
    {
        string Name { get; set; }
        int PreparationTime { get; set; }
        public bool IsReady { get; set; }
        public int Price { get; }

    }
}
