﻿using Client.Factories;
using Client.Models;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            var sampleRestaurants = InitializeSampleRestaurants();
            var factory = new RestaurantFactory(sampleRestaurants);

            Display display = new Display();
            display.PrintNotification();



            var selectedRestaurant = factory.GetRestaurant();
            display.menuDisplay(selectedRestaurant);


        }




        public class Display
        {
            private Restaurant _selectedRestaurant;
            private Menu _selectedMenu;
            public int notification = 2;
           
            public Display() { }


            public void ClearConsole()
            {
       
                int totalLines = Console.WindowHeight;
                Console.SetCursorPosition(0, notification);


                for (int i = notification; i < totalLines; i++)
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                }

                Console.SetCursorPosition(0,notification + 1);
            }

            public void PrintNotification ()
            {
                Console.WriteLine(" XXXXX");
                Console.WriteLine("                               ");
            }
            public void mainMenu()
            {
                bool isRunning = true;

                Console.WriteLine("1. Translation Service");
                Console.WriteLine("2. Food Delivery");

                while (isRunning)
                {

                }
            }
            public void menuDisplay(Restaurant selectedRestaurant)
            {
                _selectedRestaurant = selectedRestaurant;
                _selectedMenu = selectedRestaurant.getCurrentMenu();
                bool isRunning = true;
                Order order = new Order(selectedRestaurant);
                var selectedItems = new Dictionary<FoodItem, int>();
                order.OnOrderCompleted += (completedOrder) =>
                {
                    Console.WriteLine($"Order number {completedOrder.OrderNumber} is completed.");
                };

                PrintMenu();
                PrintOrder(order);

                Console.WriteLine("Select a food item number:");



                while (isRunning) 
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (char.IsDigit(keyInfo.KeyChar) && int.TryParse(keyInfo.KeyChar.ToString(), out int foodItemIndex) && foodItemIndex > 0 && foodItemIndex <= _selectedMenu.FoodItems.Count)
                    {
                        var foodItem = _selectedMenu.FoodItems[foodItemIndex - 1];
                        if (selectedItems.ContainsKey(foodItem))
                        {
                            selectedItems[foodItem]++;
                        }
                        else
                        {
                            selectedItems[foodItem] = 1;
                        }
                        order.AddFoodItem(foodItem);
                        Console.WriteLine($"{foodItem.Name} x {selectedItems[foodItem]}");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() > 0)
                    {
                        order.SendOrder();
                        Console.WriteLine("Order sent.");
                        isRunning = false;
                        ClearConsole();
                        mainMenu();
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() == 0)
                    {
                        Console.WriteLine("the basket is empty please select an item");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid food item number or 's' to place the order.");
                    }
                }

                


            }


            public void PrintMenu()
            {
                if (_selectedMenu != null)
                {
                    Console.WriteLine($"Menu for {_selectedRestaurant.Name}:");
                    for (int i = 0; i < _selectedMenu.FoodItems.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {_selectedMenu.FoodItems[i].Name} - Preparation time: {_selectedMenu.FoodItems[i].PreparationTime} minutes");
                    }

                    // Let the user choose a food item
               

                }
            }

            public void PrintOrder(Order order)
            {
                Console.WriteLine($"order n: {order.OrderNumber}");
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