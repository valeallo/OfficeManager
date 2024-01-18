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
        bool IsReady { get; set; }
        event Action OnReady;

        void MarkAsReady();
    }
}
