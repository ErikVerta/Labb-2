using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    public class Customer
    {
        public string Name { get;}
        public string Password { get; set; }

        private string _currencySign = "SEK";

        private string CurrencySign
        {
            get { return _currencySign; }
            set { _currencySign = value; }
        }

        private float _CurrencyRate = 1;

        public float CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }


        public List<Products> _cart;

        public List<Products> Cart
        {
            get { return _cart; }
            set { _cart = value; }
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
            Console.Clear();
            Console.WriteLine($"Username: {Name}");
            Console.WriteLine($"Password: {Password}");
            PrintCart();
            return "";
        }
        
        //Skriver ut Innehållet i kundvagnen samt priser.
        private void PrintCart()
        {
            var count = 0;
            Console.WriteLine("CART");
            Console.WriteLine("Product:            Price:              Amount:             Total:");

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
                    Console.Write(product.NameOfProduct);
                    Console.CursorLeft = 20;
                    Console.Write($"{product.PriceOfProduct * CurrencyRate}{CurrencySign}");
                    Console.CursorLeft = 40;
                    Console.Write(count);
                    Console.CursorLeft = 60;
                    Console.WriteLine($"{(product.PriceOfProduct * CurrencyRate) * count}{CurrencySign}");
                    count = 0;
                }
            }
            
            Console.WriteLine($"Total price for all products: {this.CalculateTotalPrice()}{CurrencySign}");
            
            Console.WriteLine("If you want to proceed to checkout press <enter>, otherwise press <backspace> to go back to Main Menu.");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Checkout();
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.MainMenu(this);
                }
            } while (true);
        }
       
        //Används för att betala. rensar också kundvagnen.
        public void Checkout()
        {
            Console.Clear();
            Console.WriteLine("CHECKOUT");
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine($"Your total: {this.CalculateTotalPrice()}{CurrencySign}");
            Cart.Clear();
            Console.WriteLine("Press any key to go back to Main Menu.");
            Console.ReadKey(true);
            Program.MainMenu(this);
        }
        
        //Kunden får upp alla produkter samt priser och kan då välja att lägga till varor i sin kundvagn.
        public void Shop()
        {
            int selectorPosition = 0;

            PrintShop(Products.Assortment, Products.Assortment[selectorPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectorPosition + 1 < Products.Assortment.Count)
                    {
                        selectorPosition++;
                        PrintShop(Products.Assortment, Products.Assortment[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectorPosition - 1 >= 0)
                    {
                        selectorPosition--;
                        PrintShop(Products.Assortment, Products.Assortment[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {                   
                    do
                    {
                        Console.Write("How many: ");
                        var input = Console.ReadLine();
                        if (int.TryParse(input, out int amount))
                        {
                            for (int i = 0; i < amount; i++)
                            {
                                Cart.Add(Products.Assortment[selectorPosition]);
                            }
                            Console.WriteLine($"{amount} added to your cart.");
                            System.Threading.Thread.Sleep(1000);
                            Shop();
                        }
                        else
                        {
                            Console.WriteLine("You didn't input a number silly, try again.");
                        }
                    } while (true);                   
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.MainMenu(this);
                }

            } while (true);
        }
        private void PrintShop(List<Products> products, Products selectedProduct) 
        {
            Console.Clear();
            Console.WriteLine("SHOP");
            Console.WriteLine("Product:            Price:");
            foreach (var product in products)
            {
                if (product == selectedProduct)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }               
                Console.Write($"{product.NameOfProduct}");
                Console.CursorLeft = 20;
                Console.WriteLine($"{product.PriceOfProduct * CurrencyRate}{CurrencySign}");
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select or press <Backspace> to go to Main Menu.");

        }

        //Räknar ut totala priset av kundvagnen.
        public virtual float CalculateTotalPrice()
        {
                     
            var total = 0F;
            foreach (var item in Cart)
            {
                total += item.PriceOfProduct * CurrencyRate;
            }
            return total;
                     
        }       
        public void CurrencyConvertToSEK()
        {
            CurrencySign = "SEK";
            CurrencyRate = 1F;
            Console.Clear();
            Console.WriteLine("Currency converted to SEK.");
            System.Threading.Thread.Sleep(1000);
            Program.MainMenu(this);
        }
        public void CurrencyConvertToDollar()
        {
            CurrencySign = "$";
            CurrencyRate = 0.2F;
            Console.Clear();
            Console.WriteLine("Currency converted to Dollar.");
            System.Threading.Thread.Sleep(1000);
            Program.MainMenu(this);
        }
        public void CurrencyConvertToPounds()
        {
            CurrencySign = "£";
            CurrencyRate = 0.1F;
            Console.Clear();
            Console.WriteLine("Currency converted to Pounds.");
            System.Threading.Thread.Sleep(1000);
            Program.MainMenu(this);
        }
    }
}
