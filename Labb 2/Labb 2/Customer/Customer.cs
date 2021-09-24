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
        public string Password { get; private set; }

        public string CustomerType { get;}

        private string _currencyCode = "SEK";

        private string CurrencyCode
        {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        private float _currencyRate = 1;

        public float CurrencyRate
        {
            get { return _currencyRate; }
            private set { _currencyRate = value; }
        }


        public List<Products> _cart;

        public List<Products> Cart
        {
            get { return _cart; }
            private set { _cart = value; }
        }
        private static List<Customer> _customerList = new List<Customer>();

        public static List<Customer> CustomerList
        {
            get { return _customerList; }
            private set { _customerList = value; }
        }

        //Konstruktorn för att skapa ett nytt konto.
        public Customer(string type, string name, string password)
        {
            CustomerType = type;
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
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Program.CreateAccount();
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.StartMenu();
                }
            } while (true);
        }
        //Metod för att verifiera lösenord, den kollar så att lösenordet stämmer med namnet som användaren skrev in i Login() metoden.
        private static void VerifyPassword(Customer customer, string username) 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("LOGIN");
                Console.WriteLine($"Username: {username}");
                Console.Write("Password: ");
                if (customer.Password == Console.ReadLine())
                {
                    Program.MainMenu(customer);
                }
                Console.Clear();
                Console.WriteLine("Wrong password.");
                Console.WriteLine("Press <enter> if you would like to try again or press <backspace> to go back to start menu.");

                ConsoleKeyInfo keyInfo;
                do
                {
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        Program.StartMenu();
                    }
                } while (true);

            }
        }

        //Skriver ut användarnamn, lösenord samt kundvagnen.
        public override string ToString()
        {                
            PrintCart();
            return $"Username: {Name}\r\nPassword: {Password}";
        }
        
        //Metod för att visa kundvagnen.
        public void ShowCart()
        {
            PrintCart();
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

        //Skriver ut Innehållet i kundvagnen samt namn och priser.
        private void PrintCart()
        {
            var productCount = 0;
            Console.Clear();
            Console.WriteLine("CART");
            Console.WriteLine("Product:            Price:              Amount:             Total:");

            foreach (var product in Products.ListofProducts)
            {              
                productCount = Cart.Count(item => item.Name == product.Name);
                if (productCount != 0)
                {
                    Console.Write(product.Name);

                    Console.CursorLeft = 20;
                    Console.Write($"{product.Price * CurrencyRate} {CurrencyCode}");

                    Console.CursorLeft = 40;
                    Console.Write(productCount);

                    Console.CursorLeft = 60;
                    Console.WriteLine($"{(product.Price * CurrencyRate) * productCount} {CurrencyCode}");
                    productCount = 0;
                }
            }          
            Console.WriteLine($"Total price for all products: {this.CalculateTotalPrice()} {CurrencyCode}");                     
        }
       
        //Metod för att betala, skriver ut lite info samt rensar kundvagnen.
        public void Checkout()
        {
            Console.Clear();
            Console.WriteLine("CHECKOUT");
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine($"Your total: {this.CalculateTotalPrice()} {CurrencyCode}");
            Cart.Clear();
            Console.WriteLine("Press any key to go back to Main Menu.");
            Console.ReadKey(true);
            Program.MainMenu(this);
        }
        
        //Metod för att lägga till olika produkter i sin kundvagn.
        public void Shop()
        {
            int pointerPosition = 0;

            PrintShop(Products.ListofProducts, Products.ListofProducts[pointerPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (pointerPosition + 1 < Products.ListofProducts.Count)
                    {
                        pointerPosition++;
                        PrintShop(Products.ListofProducts, Products.ListofProducts[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (pointerPosition - 1 >= 0)
                    {
                        pointerPosition--;
                        PrintShop(Products.ListofProducts, Products.ListofProducts[pointerPosition]);
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
                                Cart.Add(Products.ListofProducts[pointerPosition]);
                            }
                            Console.WriteLine($"{amount} added to your cart.");
                            System.Threading.Thread.Sleep(1000);
                            Shop();
                        }
                        else
                        {
                            Console.WriteLine("You didn't input a number, try again.");
                        }
                    } while (true);                   
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.MainMenu(this);
                }

            } while (true);
        }
        
        //Skriver ut alla olika produkter som finns i shopen.
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
                Console.Write($"{product.Name}");

                Console.CursorLeft = 20;
                Console.WriteLine($"{product.Price * CurrencyRate} {CurrencyCode}");
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select or press <Backspace> to go to Main Menu.");
        }

        //Räknar ut totala priset av kundvagnen.
        public virtual float CalculateTotalPrice()
        {
                     
            var total = 0F;
            foreach (var product in Cart)
            {
                total += product.Price * CurrencyRate;
            }
            return total;
                     
        }       
        
        //Ändrar valutan för det inloggade kontot.
        public void CurrencyConverter(string currencyCode, float currencyRate)
        {
            CurrencyCode = currencyCode;
            CurrencyRate = currencyRate;
            Console.Clear();
            Console.WriteLine($"Currency converted to {CurrencyCode}");
            System.Threading.Thread.Sleep(1000);
            Program.MainMenu(this);
        }
    }
}
