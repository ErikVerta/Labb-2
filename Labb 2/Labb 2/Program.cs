using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb_2
{   
    class Program
    {
        public static string Username { get; set; }
        private static int SelectorPosition { get; set; }


        private static List<string> _startMenuList = new List<string>() {"Login", "Create new account" };

        public static List<string> StartMenuList
        {
            get { return _startMenuList; }
            set { _startMenuList = value; }
        }
        private static List<string> _mainMenuList = new List<string>() {"Shop", "Go to cart", "Go to checkout", "Logout" };

        public static List<string> MainMenuList
        {
            get { return _mainMenuList; }
            set { _mainMenuList = value; }
        }



        static void Main(string[] args)
        {
            Customer customer1 = new Customer("Knatte", "123");         
            Customer customer2 = new Customer("Fnatte", "321");
            Customer customer3 = new Customer("Tjatte", "213");
            Products product1 = new Products("Korv", 5.5F);
            Products product2 = new Products("Dricka", 10F);     
            Products product3 = new Products("Äpple", 3.5F);
            SelectorPosition = 1;
            StartMenu();
        }
        
        //Första skärmen där användaren får välja att logga in eller registera ett nytt kundkonto.
        public static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("START MENU");
            var tempRowCounter = 1;
            foreach (var item in StartMenuList)
            {
                Console.SetCursorPosition(1, tempRowCounter);                            
                Console.WriteLine(item);
                tempRowCounter++;
            }          
            Console.SetCursorPosition(0, SelectorPosition);
            Console.Write(">");
            Console.SetCursorPosition(0, SelectorPosition);
            switch (Console.ReadKey().Key)
            {              
                case ConsoleKey.UpArrow:
                    if (SelectorPosition > 1)
                    {
                        SelectorPosition--;
                    }                
                    StartMenu();
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectorPosition < StartMenuList.Count())
                    {
                        SelectorPosition++;
                    }
                    StartMenu();
                    break;
                case ConsoleKey.Enter:
                    if (SelectorPosition == 1)
                    {
                        SelectorPosition = 1;
                        Customer.Login();                      
                    }
                    else if (SelectorPosition == 2)
                    {
                        SelectorPosition = 1;
                        CreateAccount();                 
                    }
                    break;
                default:
                    StartMenu();
                    break;
            }
        }
        
        //Huvudmenyn där kunden kan välja att shoppa, se kundvagn, betala eller logga ut.
        public static void MainMenu(Customer customer)
        {
            Console.Clear();
            var tempRowCounter = 1;
            Console.WriteLine("MAIN MENU");
            foreach (var item in MainMenuList)
            {
                Console.SetCursorPosition(1, tempRowCounter);
                Console.WriteLine(item);
                tempRowCounter++;
            }
            Console.SetCursorPosition(0, SelectorPosition);
            Console.Write(">");
            Console.SetCursorPosition(0, SelectorPosition);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (SelectorPosition > 1)
                    {
                        SelectorPosition--;
                    }
                    MainMenu(customer);
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectorPosition < MainMenuList.Count())
                    {
                        SelectorPosition++;
                    }
                    MainMenu(customer);
                    break;
                case ConsoleKey.Enter:
                    if (SelectorPosition == 1)
                    {
                        SelectorPosition = 1;
                        customer.Shop();
                    }
                    else if (SelectorPosition == 2)
                    {
                        SelectorPosition = 1;
                        customer.PrintCart();
                    }
                    else if (SelectorPosition == 3)
                    {
                        SelectorPosition = 1;
                        customer.Checkout();
                    }
                    else if (SelectorPosition == 4)
                    {
                        SelectorPosition = 1;
                        StartMenu();
                    }
                    break;
                default:
                    MainMenu(customer);
                    break;
            }
        }
        
        //Metod för att registrera ett nytt kundkonto. Den kollar så att namnet inte är upptaget sen skapar den en ny customer med kundens namn och lösenord.
        public static void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("CREATE ACCOUNT");
            Console.Write("Choose a Username: ");
            string inputName = Console.ReadLine();
            foreach (var customer in Customer.CustomerList)
            {
                if (customer.Name == inputName)
                {
                    Console.WriteLine("Username is taken.");
                    Console.WriteLine("Press <enter> if you would like to try again or press <backspace> to go back to start menu.");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Backspace:
                            StartMenu();
                            break;
                        case ConsoleKey.Enter:
                            CreateAccount();
                            break;
                        default:
                            StartMenu();
                            break;
                    }
                }
            }
            Console.Write("Choose a Password: ");          
            string inputPassword = Console.ReadLine();

            Customer newCustomer = new Customer(inputName, inputPassword);
            Console.WriteLine("Account created.");
            System.Threading.Thread.Sleep(2000);
            StartMenu();
        }
    }
}
