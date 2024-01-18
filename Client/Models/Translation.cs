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
        public event Action OnFinish;
        public bool IsReady { get; set; }
        private Guid Guid { get; set; }

        public Translation(string language,  int preparationTime)
        {
            Name = language;
            PreparationTime = preparationTime;
            IsReady = false;
            Guid = Guid.NewGuid();
        }


        public void MarkAsReady()
        {
            IsReady = true;
            OnFinish?.Invoke();
        }
    }
}
