using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Factories
{
    public class RestaurantFactory
    {
        private List<Restaurant> _restaurants;

        public RestaurantFactory(List<Restaurant> restaurants)
        {
            _restaurants = restaurants;
        }

        public Restaurant GetRestaurant()
        {
            int currentHour = DateTime.Now.Hour;
            string menuType;

            if (currentHour >= 6 && currentHour < 12)
            {
                menuType = "breakfast";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                menuType = "lunch";
            }
            else
            {
                menuType = "dinner";
            }

            var restaurant = _restaurants
                .Where(r => r.Menus.Any(m => m.Name.Equals(menuType, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(r => r.AvailableSpots())
                .FirstOrDefault();

            return restaurant;
        }


    }
}
