using System;

namespace Labb_2
{   
    class Program
    {
        public static string Username { get; set; }
        static void Main(string[] args)
        {
            Customer customer1 = new Customer("Knatte", "123");
            Customer.CustomerList.Add(customer1);
            Customer customer2 = new Customer("Fnatte", "321");
            Customer.CustomerList.Add(customer2);
            Customer customer3 = new Customer("Tjatte", "213");
            Customer.CustomerList.Add(customer3);
            Products product1 = new Products("Korv", 5.5F);
            Products.Assortment.Add(product1);
            Products product2 = new Products("Dricka", 10F);
            Products.Assortment.Add(product2);
            Products product3 = new Products("Äpple", 3.5F);
            Products.Assortment.Add(product3);
            StartMenu();
        }
        
        //Första skärmen där användaren får välja att logga in eller registera ett nytt kundkonto.
        public static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register new account");
            char Input = Console.ReadKey().KeyChar;
            switch (Input)
            {
                case '1':
                    Customer.Login();
                    break;
                case '2':
                    RegisterAccount();
                    break;
                default:
                    break;
            }
        }
        
        //Huvudmenyn där kunden kan välja att shoppa, se kundvagn, betala eller logga ut.
        public static void MainMenu(Customer username)
        {
            Console.Clear();
            Console.WriteLine("1: Shop");
            Console.WriteLine("2: Go to Cart");
            Console.WriteLine("3: Go to Checkout");
            Console.WriteLine("4: logout");
            char Input = Console.ReadKey().KeyChar;
            switch (Input)
            {
                case '1':
                    username.Shop(username);
                    break;
                case '2':
                    username.PrintCart(username);
                    break;
                case '3':
                    username.Checkout(username);
                    break;
                case '4':
                    Customer.Login();
                    break;
                default:
                    break;
            }
        }
        
        //Metod för att registrera ett nytt kundkonto. Den kollar så att namnet inte är upptaget sen skapar den en ny customer med kundens namn och lösenord.
        public static void RegisterAccount()
        {
            Console.Clear();
            Console.WriteLine("Choose a Username:");
            string inputName = Console.ReadLine();
            foreach (var customer in Customer.CustomerList)
            {
                if (customer.Name == inputName)
                {
                    Console.WriteLine("Username is taken.");
                    Console.WriteLine("1: Try Again.");
                    Console.WriteLine("2: Login");
                    char input = Console.ReadKey().KeyChar;
                    switch (input)
                    {
                        case '1':
                            RegisterAccount();
                            break;
                        case '2':
                            Customer.Login();
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("Choose a Password:");
            string inputPassword = Console.ReadLine();

            Customer newCustomer = new Customer(inputName, inputPassword);
            Customer.CustomerList.Add(newCustomer);
            Console.WriteLine("Account created.");
            System.Threading.Thread.Sleep(2000);
            Customer.Login();

        }
    }
}
