using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<CookingSpot> CookingSpots { get; set; }
        public List<Menu> Menus { get; set; }

        public Restaurant(string name)
        {
            Name = name;
            CookingSpots = new List<CookingSpot>
        {
            new CookingSpot(),
            new CookingSpot(),
            new CookingSpot(),
            new CookingSpot()
        };
            Menus = new List<Menu>();
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
        }

        public int AvailableSpots()
        {
            return CookingSpots.Count(spot => !spot.IsOccupied);
        }

    }
}
