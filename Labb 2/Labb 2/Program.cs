using System;

namespace Labb_2
{   
    class Program
    {
        public static string Username { get; set; }
        static void Main(string[] args)
        {
            Customer customer1 = new Customer("Knatte", "123");         
            Customer customer2 = new Customer("Fnatte", "321");
            Customer customer3 = new Customer("Tjatte", "213");
            Products product1 = new Products("Korv", 5.5F);
            Products product2 = new Products("Dricka", 10F);     
            Products product3 = new Products("Äpple", 3.5F);           
            StartMenu();
        }
        
        //Första skärmen där användaren får välja att logga in eller registera ett nytt kundkonto.
        public static void StartMenu()
        {           
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register new account");
            char Input = Console.ReadKey().KeyChar;
            switch (Input)
            {
                case '1':
                    Console.Clear();
                    Customer.Login();
                    break;
                case '2':
                    Console.Clear();
                    CreateAccount();
                    break;
                default:
                    Console.Clear();
                    StartMenu();
                    break;
            }
        }
        
        //Huvudmenyn där kunden kan välja att shoppa, se kundvagn, betala eller logga ut.
        public static void MainMenu(Customer username)
        {            
            Console.WriteLine("1: Shop");
            Console.WriteLine("2: Go to Cart");
            Console.WriteLine("3: Go to Checkout");
            Console.WriteLine("4: logout");
            char Input = Console.ReadKey().KeyChar;
            switch (Input)
            {
                case '1':
                    Console.Clear();
                    username.Shop(username);
                    break;
                case '2':
                    Console.Clear();
                    username.PrintCart(username);
                    break;
                case '3':
                    Console.Clear();
                    username.Checkout(username);
                    break;
                case '4':
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear();
                    MainMenu(username);
                    break;
            }
        }
        
        //Metod för att registrera ett nytt kundkonto. Den kollar så att namnet inte är upptaget sen skapar den en ny customer med kundens namn och lösenord.
        public static void CreateAccount()
        {          
            Console.WriteLine("Choose a Username:");
            string inputName = Console.ReadLine();
            foreach (var customer in Customer.CustomerList)
            {
                if (customer.Name == inputName)
                {
                    Console.Clear();
                    Console.WriteLine("Username is taken, try again or login.");
                    StartMenu();
                }
            }
            Console.WriteLine("Choose a Password:");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Confirm your Password:");
            string inputPassword2 = Console.ReadLine();
            if (inputPassword2 == inputPassword)
            {
                Customer newCustomer = new Customer(inputName, inputPassword);
                Customer.CustomerList.Add(newCustomer);
                Console.WriteLine("Account created.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                StartMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Passwords didn't match. Try again or login.");
                StartMenu();
            }
        }
    }
}
