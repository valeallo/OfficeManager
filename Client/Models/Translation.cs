using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Translation
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public event Action OnCooked;
        public bool IsCooked { get; set; }

        public Translation(string name, int preparationTime)
        {
            Name = name;
            PreparationTime = preparationTime;
            IsCooked = false;
        }


        public void MarkAsCooked()
        {
            IsCooked = true;
            OnCooked?.Invoke();
        }
    }
}
