using Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Translation : IPreparableItem
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public event Action OnReady;
        public bool IsReady { get; set; }

        public Translation(string language,  int preparationTime)
        {
            Name = language;
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
