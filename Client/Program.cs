using Client.Factories;
using Client.Models;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var sampleRestaurants = InitializeSampleRestaurants();

            var factory = new RestaurantFactory(sampleRestaurants);

            while (true)
            {
                Console.WriteLine("Enter menu type (breakfast, lunch, dinner): ");
                string menuType = Console.ReadLine();

                var selectedRestaurant = factory.GetRestaurant(menuType);
                if (selectedRestaurant != null)
                {
                    // Display the menu for the selected type
                    var menu = selectedRestaurant.Menus.FirstOrDefault(m => m.Name.Equals(menuType, StringComparison.OrdinalIgnoreCase));
                    if (menu != null)
                    {
                        Console.WriteLine($"Menu for {menuType}:");
                        for (int i = 0; i < menu.FoodItems.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {menu.FoodItems[i].Name} - Preparation time: {menu.FoodItems[i].PreparationTime} minutes");
                        }

                        // Let the user choose a food item
                        Console.WriteLine("Select a food item number:");
                        if (int.TryParse(Console.ReadLine(), out int foodItemIndex) && foodItemIndex > 0 && foodItemIndex <= menu.FoodItems.Count)
                        {
                            var foodItem = menu.FoodItems[foodItemIndex - 1];

                            // Process the order if a cooking spot is available
                            var availableSpot = selectedRestaurant.CookingSpots.FirstOrDefault(spot => !spot.IsOccupied);
                            if (availableSpot != null)
                            {
                                availableSpot.CookFoodItem(foodItem);
                                Console.WriteLine($"Order for {foodItem.Name} is being processed...");
                                // Add more logic for handling the cooking time and order completion
                            }
                            else
                            {
                                Console.WriteLine("No available cooking spots in the selected restaurant.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid food item selection.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No menu available for {menuType} in this restaurant.");
                    }
                }
                else
                {
                    Console.WriteLine("No restaurant available for the selected menu type.");
                }
            }
            // Additional logic for order completion and spot availability
        }



        public static List<Restaurant> InitializeSampleRestaurants()
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