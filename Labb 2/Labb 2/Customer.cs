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
            _customerList.Add(this);
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
                        Console.Clear();
                        Program.MainMenu(CustomerList[i]);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong password, try again or create a new account.");
                        Program.StartMenu();
                    }
                }
            }
            Console.Clear();
            Console.WriteLine($"There isn't an account with the username: {inputName}, try again or create a new account."); 
            Program.StartMenu();

        }

        //Skriver ut användarnamn, lösenord samt kundvagnen.
        public override string ToString()
        {
            Console.WriteLine(Name);
            Console.WriteLine(Password);
            return "";
        }
        
        //Skriver ut Innehållet i kundvagnen samt priser.
        public void PrintCart(Customer username)
        {           
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
                    Console.Clear();
                    username.Shop(username);
                    break;
                case '2':
                    Console.Clear();
                    username.Checkout(username);
                    break;
                case '3':
                    Console.Clear();
                    Program.StartMenu();
                    break;
                default:
                    Console.Clear();
                    username.PrintCart(username);
                    break;
            }
        }
       
        //Används för att betala. rensar också kundvagnen.
        public void Checkout(Customer username)
        {          
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine($"Your total: {CalculateTotalPrice(username)}SEK");
            username.Cart.Clear();
            Program.MainMenu(username);
        }
        
        //Kunden får upp alla produkter samt priser och kan då välja att lägga till varor i sin kundvagn.
        public void Shop(Customer username)
        {
            Console.Clear();
            foreach (var item in Products.Assortment)
            {
                Console.WriteLine($"{item.NameOfProduct} Price: {item.PriceOfProduct}SEK");
            }
            Console.WriteLine("Write the name of the Item you would like to buy. If you want to go to the main meny press ENTER.");
            string inputItem = Console.ReadLine().ToLower();
            if (inputItem != "")
            {
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
                        System.Threading.Thread.Sleep(2000);
                        break;
                    }
                }
            }
            Console.Clear();
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
