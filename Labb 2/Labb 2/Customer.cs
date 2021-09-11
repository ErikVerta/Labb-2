using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    class Customer
    {
        public string Name { get; set; }
        private string Password { get; set; }
        private List<Products> _cart;

        public List<Products> Cart
        {
            get { return _cart; }
            private set { _cart = value; }
        }
        private static List<Customer> _customerList = new List<Customer>();

        public static List<Customer> CustomerList
        {
            get { return _customerList; }
            set { _customerList = value; }
        }

        //Konstruktor för att skapa ett nytt kundkonto.
        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            Cart = new List<Products>();          
        }
        //Metod för att logga in. Den frågar efter namn och lösenord, sedan kollar den om det finns ett konto med det namnet, kollar sedan om lösenordet stämmer med namnet.
        //Om man lyckas logga in så skickas man till MainMenu, annars får man ett val om man vill registrera nytt konto eller försöka igen.
        public static void Login()
        {
            Console.WriteLine("Username:");
            var inputName = Console.ReadLine();
            Console.WriteLine("Password:");
            var inputPassword = Console.ReadLine();

            for (int i = 0; i < CustomerList.Count; i++)
            {
                if (CustomerList[i].Name == inputName)
                {
                    if (CustomerList[i].Password == inputPassword)
                    {
                        Program.MainMenu(CustomerList[i]);
                    }
                    else
                    {
                        Console.WriteLine("Wrong password.");
                        Console.WriteLine("1: try again.");
                        Console.WriteLine("2: Register new account.");

                        var inputChoise = Console.ReadKey().KeyChar;
                        switch (inputChoise)
                        {
                            case '1':
                                Console.Clear();
                                Login();
                                break;
                            case '2':
                                Program.RegisterAccount();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            Console.WriteLine($"There isn't an account with the username: {inputName}");
            Console.WriteLine("1: try again.");
            Console.WriteLine("2: Register new account.");

            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    Console.Clear();
                    Login();
                    break;
                case '2':
                    Program.RegisterAccount();
                    break;
                default:
                    break;
            }
        }
        
        //Skriver ut användarnamn, lösenord samt kundvagnen.
        public void ToString()
        {

        }
        
        //Skriver ut Innehållet i kundvagnen samt priser.
        public void PrintCart(Customer username)
        {
            Console.Clear();
            var count = 0;
            foreach (var product in Products.Assortment)
            {
                foreach (var item in username.Cart)
                {
                    if (item == product)
                    {
                        count++;
                    }
                }
                if (count != 0)
                {
                    Console.WriteLine($"{product.NameOfProduct} Price for 1: {product.PriceOfProduct}SEK Price for {count}: {product.PriceOfProduct * count}");
                    count = 0;
                }
            }
            Console.WriteLine($"Total price: {username.CalculateTotalPrice(username)}");
            
            Console.WriteLine("1: Shop");
            Console.WriteLine("2: Checkout");
            Console.WriteLine("3: logout");
            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    username.Shop(username);
                    break;
                case '2':
                    username.Checkout(username);
                    break;
                case '3':
                    Program.StartMenu();
                    break;
                default:
                    break;
            }
        }
       
        //Används för att betala. rensar också kundvagnen.
        public void Checkout(Customer username)
        {
            Console.Clear();
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine($"Your total: {CalculateTotalPrice(username)}SEK");
            username.Cart.Clear();
            Console.WriteLine("1: Shop");
            Console.WriteLine("2: Logout");
            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    username.Shop(username);
                    break;
                case '2':
                    Program.StartMenu();
                    break;
                default:
                    break;
            }
        }
        
        //Kunden får upp alla produkter samt priser och kan då välja att lägga till varor i sin kundvagn.
        public void Shop(Customer username)
        {
            Console.Clear();
            foreach (var item in Products.Assortment)
            {
                Console.WriteLine($"{item.NameOfProduct}, Price: {item.PriceOfProduct}SEK");
            }
            Console.WriteLine("Write the name of the Item you would like to buy.");
            string inputItem = Console.ReadLine().ToLower();
            foreach (var item in Products.Assortment)
            {
                if (item.NameOfProduct.ToLower() == inputItem)
                {
                    Console.WriteLine("How many would you like to buy?");
                    int inputAmount = int.Parse(Console.ReadLine());
                    for (int j = 0; j < inputAmount; j++)
                    {
                        username.Cart.Add(item);
                    }
                    Console.WriteLine($"{inputAmount} {item.NameOfProduct} added to your cart.");
                    break;
                }
            }
            System.Threading.Thread.Sleep(2000);
            Program.MainMenu(username);
        }
        
        //Räknar ut totala priset av kundvagnen.
        private float CalculateTotalPrice(Customer username)
        {
            var total = 0F;
            foreach (var item in username.Cart)
            {
                total += item.PriceOfProduct;
            }
            return total;
        }

    }
}
