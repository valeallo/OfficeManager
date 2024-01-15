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

        public Restaurant GetRestaurant(string menuType)
        {
            // Select a restaurant that has a menu matching the requested menu type
            // and has the highest number of available cooking spots
            return _restaurants
                .Where(r => r.Menus.Any(m => m.Name.Equals(menuType, StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(r => r.AvailableSpots())
                .FirstOrDefault();
        }

        
    }
}
