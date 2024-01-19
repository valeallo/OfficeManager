using Client.enums;
using Client.Offices;
using Client.Models;
using static Client.Program;
using Client.Interface;

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
     

            public int notificationSpace = 2;
            List<IPreparableItem> _currentOrder = new List<IPreparableItem>();
           
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
                            TranslationOffice translationOffice = (TranslationOffice)new OfficeManager(translationService).CreateService();
                            translationDisplay(translationOffice);
                            break;
                        case '2':
                            ServiceType restaurantService = ServiceType.RestaurantService;
                            DeliveryOffice deliveryOffice = (DeliveryOffice)new OfficeManager(restaurantService).CreateService();
                   
                            deliveryDisplay(deliveryOffice);
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
            public void deliveryDisplay(DeliveryOffice office)
            {
               
                Restaurant selectedRestaurant = office.GetServices();
                Menu selectedMenu = selectedRestaurant.getCurrentMenu();
                bool isRunning = true;
                Order order = new Order(office);
                var selectedItems = new Dictionary<FoodItem, int>();
                order.OnOrderCompleted += (completedOrder) =>
                {
                    Console.WriteLine($"Order number {completedOrder.OrderNumber} is completed.");
                };

                PrintMenu(office, order);





                while (isRunning) 
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (char.IsDigit(keyInfo.KeyChar) && int.TryParse(keyInfo.KeyChar.ToString(), out int foodItemIndex) && foodItemIndex > 0 && foodItemIndex <= selectedMenu.FoodItems.Count)
                    {
                        var foodItem = selectedMenu.FoodItems[foodItemIndex - 1];
                        var newItem = new FoodItem(foodItem.Name, foodItem.PreparationTime, foodItem.Price);
                        if (selectedItems.ContainsKey(foodItem))
                        {
                            selectedItems[foodItem]++;
                        }
                        else
                        {
                            selectedItems[foodItem] = 1;
                        }
                        order.AddItem(newItem);
                       _currentOrder.Add(foodItem);
                        PrintMenu(office, order);
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() > 0)
                    {
                        office.SendOrder(order);
                        Console.WriteLine("Order sent.");
                        isRunning = false;
                        _currentOrder.Clear();
                        mainMenu();
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() == 0)
                    {
                        Console.WriteLine("the basket is empty please select an item");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 'b')
                    {
                        _currentOrder.Clear();
                        mainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid food item number or 's' to place the order.");
                    }
                }

                


            }

            public void translationDisplay(TranslationOffice office)
            {
                TranslationProvider provider = office.GetServices();
                ClearConsole();
                bool isRunning = true;
                Order order = new Order(office);
                List<Translation> translations = provider.getTranslations();
                var selectedItems = new Dictionary<Translation, int>();
                order.OnOrderCompleted += (completedOrder) =>
                {
                    Console.WriteLine($"Order number {completedOrder.OrderNumber} is completed.");

                };

                PrintAllTranslations(provider, order);
               
   



                while (isRunning)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (char.IsDigit(keyInfo.KeyChar) && int.TryParse(keyInfo.KeyChar.ToString(), out int ItemIndex) && ItemIndex > 0 && ItemIndex <= translations.Count)
                    {
                        var item = translations[ItemIndex - 1];
                        var newItem = new Translation(item.Name, item.PreparationTime, item.Price);
                        if (selectedItems.ContainsKey(item))
                        {
                            selectedItems[item]++;
                        }
                        else
                        {
                            selectedItems[item] = 1;
                        }
                        order.AddItem(newItem);
                        _currentOrder.Add(newItem);

                        PrintAllTranslations(provider, order);
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() > 0)
                    {
                        office.SendOrder(order);
                        Console.WriteLine("Order sent.");
                        isRunning = false;
                        _currentOrder.Clear();
                        mainMenu();
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 's' && selectedItems.Count() == 0)
                    {
                        Console.WriteLine("the basket is empty please select an item");
                    }
                    else if (char.ToLower(keyInfo.KeyChar) == 'b')
                    {
                        _currentOrder.Clear();
                        mainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a valid food item number or 's' to place the order.");
                    }
                }

            }
            public void PrintMenu(DeliveryOffice office, Order order)
            {
                ClearConsole();
                Restaurant selectedRestaurant = office.GetServices();
                Menu selectedMenu = selectedRestaurant.getCurrentMenu();
                if (selectedMenu != null)
                {
                    Console.WriteLine($"Menu for {selectedRestaurant.Name}:");
                    for (int i = 0; i < selectedMenu.FoodItems.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {selectedMenu.FoodItems[i].Name} - Preparation time: {selectedMenu.FoodItems[i].PreparationTime} minutes - Price {selectedMenu.FoodItems[i].Price} $");
                    }
               

                }
                Console.WriteLine("Press 's' to send order press 'b' to go back");
                Console.WriteLine("Select a food  number:");
                PrintOrder(order);
                PrintCurrentOrder();
            }


            public void PrintAllTranslations(TranslationProvider provider, Order order)
            {
                ClearConsole();
                List<Translation> list = provider.getTranslations();
                for (int i = 0; i < list.Count; i++)
                {
                    Translation translation = list[i]; 
                    Console.WriteLine($"{i + 1}. {translation.Name} - Preparation time in minutes x page {translation.PreparationTime} - Price x page {translation.Price}");
                }
                Console.WriteLine("Press 's' to send order press 'b' to go back");
                Console.WriteLine("Select a food  number:");
                PrintOrder(order);
                PrintCurrentOrder();
            }

            public void PrintOrder(Order order)
            {
                Console.WriteLine($"order n: {order.OrderNumber}");
            }
            public void PrintCurrentOrder()
            {
                if (_currentOrder.Count == 0)
                {
                    Console.WriteLine("Current order is empty.");
                    return;
                }

                var itemCounts = new Dictionary<string, int>();
                double totalPrice = 0; 

                foreach (var item in _currentOrder)
                {
                    if (itemCounts.ContainsKey(item.Name))
                    {
                        itemCounts[item.Name]++;
                    }
                    else
                    {
                        itemCounts[item.Name] = 1;
                    }
                    totalPrice += item.Price; 
                }

                Console.WriteLine("Current Order:");
                foreach (var item in itemCounts)
                {
                    Console.WriteLine($"{item.Key} - Quantity: {item.Value}");
                }

                Console.WriteLine($"Total Price: {totalPrice} $"); 
            }
        }
        }

  

    }
    