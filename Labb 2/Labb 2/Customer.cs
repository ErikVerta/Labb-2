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
        private static int _selectorPosition = 2;

        private static int SelectorPosition
        {
            get { return _selectorPosition; }
            set { _selectorPosition = value; }
        }

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
        //Metod för att logga in. Den frågar efter username och kollar om username finns registrerad.
        public static void Login()
        {           
            Console.Clear();
            Console.WriteLine("LOGIN");
            Console.Write("Username: ");
            var inputName = Console.ReadLine();
            foreach (var customer in CustomerList)
            {
                if (customer.Name == inputName)
                {
                    VerifyPassword(customer, customer.Name);
                }
            }
            Console.Clear();
            Console.WriteLine($"There isn't an account with the username: {inputName}");
            Console.WriteLine("Press <enter> if you would like to create a new account or press <backspace> to go back to start menu.");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Backspace:
                    Program.StartMenu();
                    break;
                case ConsoleKey.Enter:
                    Program.CreateAccount();
                    break;
                default:
                    Program.StartMenu();
                    break;
            }
            Program.StartMenu();

        }
        //Metod för att verifiera lösenord, den tar in en customer från login metoden för att kontrollera att lösenordet men skriver in stämmer med propertyn password i customer.
        private static void VerifyPassword(Customer customer, string username) 
        {
            bool validPassword = false;
            while (!validPassword)
            {
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.WriteLine($"Username: {username}");
                Console.Write("Password: ");
                var inputPassword = Console.ReadLine();
                if (customer.Password == inputPassword)
                {
                    validPassword = true;
                    Program.MainMenu(customer);
                }
                Console.Clear();
                Console.WriteLine("Wrong password.");
                Console.WriteLine("Press <enter> if you would like to try again or press <backspace> to go back to start menu.");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Backspace:
                        Program.StartMenu();
                        break;
                    case ConsoleKey.Enter:
                        break;
                    default:
                        Program.StartMenu();
                        break;
                }
            }
        }

        //Skriver ut användarnamn, lösenord samt kundvagnen.
        public override string ToString()
        {
            Console.WriteLine(Name);
            Console.WriteLine(Password);
            return "";
        }
        
        //Skriver ut Innehållet i kundvagnen samt priser.
        public void PrintCart()
        {
            Console.Clear();
            var count = 0;
            var tempRowCounter = 2;
            Console.WriteLine("CART");
            Console.SetCursorPosition(0, 1);
            Console.Write("Product:");
            Console.SetCursorPosition(10, 1);
            Console.Write("Price:");
            Console.SetCursorPosition(20, 1);
            Console.Write("Amount:");
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("Total:");

            foreach (var product in Products.Assortment)
            {
                foreach (var item in Cart)
                {
                    if (item == product)
                    {
                        count++;
                    }
                }
                if (count != 0)
                {
                    Console.SetCursorPosition(0, tempRowCounter);
                    Console.Write(product.NameOfProduct);
                    Console.SetCursorPosition(10, tempRowCounter);
                    Console.Write($"{product.PriceOfProduct} SEK");
                    Console.SetCursorPosition(20, tempRowCounter);
                    Console.Write(count);
                    Console.SetCursorPosition(30, tempRowCounter);
                    Console.WriteLine($"{product.PriceOfProduct * count} SEK");
                    tempRowCounter++;
                    count = 0;
                }
            }
            Console.WriteLine($"Total price for all products: {CalculateTotalPrice()} SEK");

            Console.WriteLine("If you want to proceed to checkout press <enter>, otherwise press <backspace> to go back to Main Menu.");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Backspace:
                    Program.MainMenu(this);
                    break;               
                case ConsoleKey.Enter:
                    Checkout();
                    break;                       
                default:
                    Program.MainMenu(this);
                    break;
            }
        }
       
        //Används för att betala. rensar också kundvagnen.
        public void Checkout()
        {
            Console.Clear();
            Console.WriteLine("CHECKOUT");
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine($"Your total: {CalculateTotalPrice()} SEK");
            Cart.Clear();
            Console.WriteLine("Press any key to go back to Main Menu.");
            Console.ReadKey(true);
            Program.MainMenu(this);
        }
        
        //Kunden får upp alla produkter samt priser och kan då välja att lägga till varor i sin kundvagn.
        public void Shop()
        {
            var tempRowCounter = 2;
            Console.Clear();
            Console.WriteLine("SHOP");
            Console.SetCursorPosition(0, 1);
            Console.Write("Product:");
            Console.SetCursorPosition(10, 1);
            Console.WriteLine("Price:");
            foreach (var product in Products.Assortment)
            {
                Console.SetCursorPosition(1, tempRowCounter);
                Console.Write(product.NameOfProduct);
                Console.SetCursorPosition(10, tempRowCounter);
                Console.WriteLine($"{product.PriceOfProduct} SEK");
                tempRowCounter++;
            }
            Console.WriteLine("Navigate to your product of choise and press <enter>, if you want to go back to the Main Menu press <backspace>");
            Console.SetCursorPosition(0, SelectorPosition);
            Console.Write(">");
            Console.SetCursorPosition(0, SelectorPosition);
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (SelectorPosition > 2)
                    {
                        SelectorPosition--;
                    }
                    Shop();
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectorPosition - 1 < Products.Assortment.Count())
                    {
                        SelectorPosition++;
                    }
                    Shop();
                    break;
                case ConsoleKey.Enter:
                    Console.SetCursorPosition(20, SelectorPosition);
                    Console.Write("Amount: ");
                    var amountOfProduct = int.Parse(Console.ReadLine());
                    for (int i = 0; i < amountOfProduct; i++)
                    {
                        Cart.Add(Products.Assortment.ElementAt(SelectorPosition - 2));
                    }
                    Console.SetCursorPosition(30 + amountOfProduct.ToString().Length, SelectorPosition);
                    Console.Write("added to your cart.");
                    System.Threading.Thread.Sleep(1000);
                    Shop();
                    break;
                case ConsoleKey.Backspace:
                    Program.MainMenu(this);
                    break;
                default:
                    Shop();
                    break;
            }
        }
        
        //Räknar ut totala priset av kundvagnen.
        private float CalculateTotalPrice()
        {
            var total = 0F;
            foreach (var item in Cart)
            {
                total += item.PriceOfProduct;
            }
            return total;
        }

    }
}
