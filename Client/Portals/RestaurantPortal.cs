using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Portals
{
    public class RestaurantPortal
    {
        private List<Restaurant> _restaurants;
        private static RestaurantPortal _instance;

        private RestaurantPortal()
        {
            var sampleRestaurants = InitializeSample();
            _restaurants = sampleRestaurants.ToList();
            
        }

        public static RestaurantPortal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestaurantPortal();
                }
                return _instance;
            }
        }
        public Restaurant GetService()
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
        public static List<Restaurant> InitializeSample()
        {
            var restaurants = new List<Restaurant>();

            // Restaurant 1
            var restaurant1 = new Restaurant("BreakfastPlace");
            var breakfastMenu1 = new Menu("breakfast");
            breakfastMenu1.AddFoodItem(new FoodItem("Pancakes", 10));
            breakfastMenu1.AddFoodItem(new FoodItem("Omelette", 15));
            restaurant1.AddMenu(breakfastMenu1);
            restaurants.Add(restaurant1);

            // Restaurant 2
            var restaurant2 = new Restaurant("LunchDiner");
            var lunchMenu2 = new Menu("lunch");
            lunchMenu2.AddFoodItem(new FoodItem("Pizza", 25));
            lunchMenu2.AddFoodItem(new FoodItem("Patatine", 20));
            restaurant2.AddMenu(lunchMenu2); // Add the lunch menu to restaurant2
            restaurants.Add(restaurant2);    // Add restaurant2 to the list

            // Restaurant 3
            var restaurant3 = new Restaurant("DinnerGrill");
            var dinnerMenu3 = new Menu("dinner");
            dinnerMenu3.AddFoodItem(new FoodItem("Steak", 25));
            dinnerMenu3.AddFoodItem(new FoodItem("Pasta", 20));
            restaurant3.AddMenu(dinnerMenu3);
            restaurants.Add(restaurant3);

            return restaurants;

        }


    }
}
