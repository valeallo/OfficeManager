﻿using Client.enums;
using Client.Factories;
using Client.Models;
using static Client.Program;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            Display display = new Display();
            display.PrintNotification(" ");
            display.mainMenu();



        }




        public class Display
        {
            private Restaurant _selectedRestaurant;
            private Menu _selectedMenu;
            public int notificationSpace = 2;

           
            public Display() { }


            public void ClearConsole()
            {
       
                int totalLines = Console.WindowHeight;
                Console.SetCursorPosition(0, notificationSpace);


                for (int i = notificationSpace; i < totalLines; i++)
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                }

                Console.SetCursorPosition(0,notificationSpace + 1);
            }

            public void PrintNotification (string notificationString)
            {
       
                int currentLine = Console.CursorTop;
                int currentColumn = Console.CursorLeft;

                Console.SetCursorPosition(0, 0); 
                Console.WriteLine(notificationString);
                Console.Write(new string(' ', Console.WindowWidth - notificationString.Length));
                Console.SetCursorPosition(currentColumn, currentLine);
                if (currentLine == 0)
                {
                    Console.SetCursorPosition(currentColumn, currentLine + 2);
                }
            }
            public void mainMenu()
            {
                ClearConsole();
                bool isRunning = true;

        
                Console.WriteLine("1. Translation Service");
                Console.WriteLine("2. Food Delivery");
                Console.WriteLine("Q. Exit");


                var input = char.ToUpper(Console.ReadKey().KeyChar);
                while (isRunning)
                {

                    Console.WriteLine();


                    switch (input)
                    {
                        case '1':
                            ServiceType translationService = ServiceType.TranslationService;
                            TranslationProvider translationProvider = (TranslationProvider)new OfficeManager(translationService).CreateService();
                            translationDisplay(translationProvider);
                            break;
                        case '2':
                            ServiceType restaurantService = ServiceType.RestaurantService;
                            Restaurant selectedRestaurant = (Restaurant)new OfficeManager(restaurantService).CreateService();
                   
                            deliveryDisplay(selectedRestaurant);
                            break;
                        case 'Q':
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            Console.ReadKey();
                            break;
                    }


                }
            }
            public void deliveryDisplay(Restaurant selectedRestaurant)
            {
                ClearConsole();
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
                        order.AddItem(foodItem);
                        Console.WriteLine($"{foodItem.Name} x {selectedItems[foodItem]}");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() > 0)
                    {
                        order.SendOrder();
                        Console.WriteLine("Order sent.");
                        isRunning = false;
                        mainMenu();
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() == 0)
                    {
                        Console.WriteLine("the basket is empty please select an item");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 'b')
                    {
                        mainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid food item number or 's' to place the order.");
                    }
                }

                


            }

            public void translationDisplay(TranslationProvider provider)
            {
                ClearConsole();
                bool isRunning = true;
                Order order = new Order(provider);
                List<Translation> translations = provider.getTranslations();
                var selectedItems = new Dictionary<Translation, int>();
                order.OnOrderCompleted += (completedOrder) =>
                {
                    Console.WriteLine($"Order number {completedOrder.OrderNumber} is completed.");

                };

                PrintAllTranslations(provider);
                PrintOrder(order);

                Console.WriteLine("Select a food item number:");



                while (isRunning)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (char.IsDigit(keyInfo.KeyChar) && int.TryParse(keyInfo.KeyChar.ToString(), out int foodItemIndex) && foodItemIndex > 0 && foodItemIndex <= translations.Count)
                    {
                        var item = translations[foodItemIndex - 1];
                        if (selectedItems.ContainsKey(item))
                        {
                            selectedItems[item]++;
                        }
                        else
                        {
                            selectedItems[item] = 1;
                        }
                        order.AddItem(item);
                        Console.WriteLine($"{item.Name} x {selectedItems[item]}");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() > 0)
                    {
                        order.SendOrder();
                        Console.WriteLine("Order sent.");
                        isRunning = false;
                        mainMenu();
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() == 0)
                    {
                        Console.WriteLine("the basket is empty please select an item");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 'b')
                    {
                        mainMenu();
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
               

                }
            }


            public void PrintAllTranslations(TranslationProvider provider)
            {
                List<Translation> list = provider.getTranslations();
                for (int i = 0; i < list.Count; i++)
                {
                    Translation translation = list[i]; 
                    Console.WriteLine($"{i + 1}- {translation.Name} - {translation.PreparationTime}");
                }
            }

            public void PrintOrder(Order order)
            {
                Console.WriteLine($"order n: {order.OrderNumber}");
            }
        }

  

    }
    }