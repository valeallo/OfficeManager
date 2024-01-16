using Client.Factories;
using Client.Models;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            var sampleRestaurants = InitializeSampleRestaurants();
            var factory = new RestaurantFactory(sampleRestaurants);

         
             
            var selectedRestaurant = factory.GetRestaurant();
            display.menuDisplay(selectedRestaurant);


        }




        public class Display
        {
            public Display() { }
            public void menuDisplay(Restaurant selectedRestaurant)
            {
                while(true) {
                if (selectedRestaurant != null)
                {
                    // Display the menu for the selected type
                    var menu = selectedRestaurant.getCurrentMenu();
                    if (menu != null)
                    {
                        Console.WriteLine($"Menu for {selectedRestaurant.Name}:");
                        for (int i = 0; i < menu.FoodItems.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {menu.FoodItems[i].Name} - Preparation time: {menu.FoodItems[i].PreparationTime} minutes");
                        }
                        Order order = new Order(selectedRestaurant);
                        order.OnOrderCompleted += (completedOrder) =>
                        {
                            Console.WriteLine($"Order number {completedOrder.OrderNumber} is completed.");
                        };
                        // Let the user choose a food item
                        Console.WriteLine("Select a food item number:");

                        if (int.TryParse(Console.ReadLine(), out int foodItemIndex) && foodItemIndex > 0 && foodItemIndex <= menu.FoodItems.Count)
                        {
                            var foodItem = menu.FoodItems[foodItemIndex - 1];
                            order.AddFoodItem(foodItem);
                            order.SendOrder();

                        }
                        else if (Console.ReadLine() == "send")
                        {
                            Console.WriteLine("No food item selected. Please select a valid food item.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid food item selection.");
                        }
                    }

                }

                }
            }
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