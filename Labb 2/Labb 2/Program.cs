using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labb_2
{   
    class Program
    {
                   
        private static List<StartMenuOption> StartMenuOptions { get; set; }

        private static List<MainMenuOption> MainMenuOptions { get; set; }

        private static List<CurrencyOptions> CurrencyOptions { get; set; }

        //Instansierar alla nödvändiga objekt.
        static void Main(string[] args)
        {
            ReadTextfile();

            new Products("Apple", 3.5F);
            new Products("Hotdog", 5F);
            new Products("Soda", 10);

            StartMenuOptions = new List<StartMenuOption>
            {
                new StartMenuOption("Login", () => Customer.Login()),
                new StartMenuOption("Create new account", () => CreateAccount()),
            };

            MainMenuOptions = new List<MainMenuOption> 
            {
                new MainMenuOption("Shop", (customer) => customer.Shop()),
                new MainMenuOption("Go to Cart", (customer) => customer.ToString()),
                new MainMenuOption("Go to Checkout", (customer) => customer.Checkout()),
                new MainMenuOption("Change currency", (customer) => CurrencyMenu(customer)),
                new MainMenuOption("Logout", (customer) => StartMenu()),                 
            };

            CurrencyOptions = new List<CurrencyOptions>
            {
                new CurrencyOptions("Change to SEK",(customer) => customer.CurrencyConvertToSEK()),
                new CurrencyOptions("Change to USD",(customer) => customer.CurrencyConvertToUSD()),
                new CurrencyOptions("Change to GBP", (customer) => customer.CurrencyConvertToGBP()),
            };
           
            StartMenu();
        }
        
        //Startmenyn med alternativ för att logga in eller registrera nytt konto.
        public static void StartMenu()
        {
            int pointerPosition = 0;

            PrintStartMenu(StartMenuOptions, StartMenuOptions[pointerPosition]);
     
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (pointerPosition + 1 < StartMenuOptions.Count)
                    {
                        pointerPosition++;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (pointerPosition - 1 >= 0)
                    {
                        pointerPosition--;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    StartMenuOptions[pointerPosition].Selected.Invoke();
                }               
            } while (true);
        }
        //Skriver ut start menyn.
        private static void PrintStartMenu(List<StartMenuOption> options, StartMenuOption selectedOption)
        {
            Console.Clear();
            Console.WriteLine("START MENU");
            foreach (var option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.WriteLine(option.Name);
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select.");
        }
        //Mainmenyn med alternativ för att shoppa, gå till kundvagn, gå till kassa, ändra valuta eller logga ut.
        public static void MainMenu(Customer customer)
        {
            int pointerPosition = 0;

            PrintMainMenu(MainMenuOptions, MainMenuOptions[pointerPosition]);
         
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (pointerPosition + 1 < MainMenuOptions.Count)
                    {
                        pointerPosition++;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (pointerPosition - 1 >= 0)
                    {
                        pointerPosition--;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    MainMenuOptions[pointerPosition].Selected.Invoke(customer);
                }

            } while (true);
        }
        
        //skriver ut mainmenyn.
        private static void PrintMainMenu(List<MainMenuOption> options, MainMenuOption selectedOption)
        {
            Console.Clear();
            Console.WriteLine("MAIN MENU");
            foreach (var option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.WriteLine(option.Name);
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select.");

        }
        
        //Valutamenyn med alternativ för att byta till Euro, Dollar eller SEK.
        private static void CurrencyMenu(Customer customer)
        {
            int pointerPosition = 0;

            PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[pointerPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (pointerPosition + 1 < CurrencyOptions.Count)
                    {
                        pointerPosition++;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (pointerPosition - 1 >= 0)
                    {
                        pointerPosition--;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[pointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    CurrencyOptions[pointerPosition].Selected.Invoke(customer);
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    MainMenu(customer);
                }
            } while (true);
        }
        
        //Skriver ut valutamenyn.
        private static void PrintCurrencyMenu(List<CurrencyOptions> options, CurrencyOptions selectedOption)
        {
            Console.Clear();
            Console.WriteLine("CURRENCY MENU");
            foreach (var option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.WriteLine(option.Name);
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select or press <Backspace> to go to Main Menu.");
        }

        //Metod för att registrera ett nytt kundkonto. Kollar så att namnet inte är upptaget sen skapar den en ny customer med kundens namn och lösenord.
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
                    Console.WriteLine($"Username: {inputName} is taken.");
                    Console.WriteLine("Press <enter> if you would like to try again or press <backspace> to go back to start menu.");

                    ConsoleKeyInfo keyInfo;
                    do
                    {
                        keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            CreateAccount();
                        }
                        if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            StartMenu();
                        }
                    } while (true);
                }
            }
            Console.Write("Choose a Password: ");          
            string inputPassword = Console.ReadLine();

            new Customer(inputName, inputPassword);
            Console.WriteLine("Account created.");
            WriteTextfile();
            System.Threading.Thread.Sleep(2000);
            StartMenu();
        }
        
        //Skriver namn och lösenord för alla konton till en .txt fil.
        private static void WriteTextfile()
        {
            List<string> accountInformation = new List<string>();
            foreach (var customer in Customer.CustomerList)
            {
                accountInformation.Add($"{customer.Name},{customer.Password},");
            }

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string path = Path.Combine(docPath, "Labb2AccountInfo.txt");

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                foreach (var line in accountInformation)
                    outputFile.Write(line);
            }
        }

        //Instansierar Customer objekt med namn och lösenord som den har läst in från en .txt fil.      
        private static void ReadTextfile()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string path = Path.Combine(docPath, "Labb2AccountInfo.txt");

            CheckForTextFile(path);

            using (var sr = new StreamReader(path))
            {
                var text = sr.ReadLine();
               
                
                List<string> names = text.Split(',').ToList<string>();
                names.Remove("");
                for (int i = 0; i < names.Count(); i += 2)
                {
                    if (names[i] == "Knatte")
                    {
                        new GoldCustomer(names[i], names[i + 1]);
                    }
                    else if (names[i] == "Fnatte")
                    {
                        new SilverCustomer(names[i], names[i + 1]);
                    }
                    else if (names[i] == "Tjatte")
                    {
                        new BronzeCustomer(names[i], names[i + 1]);
                    }
                    else
                    {
                        new Customer(names[i], names[i + 1]);
                    }                  
                }
                
            }           
        }
        
        //Kontrollerar om .txt filen finns på datorn, om den inte finns så skapar den .txt filen med standard konton.
        private static void CheckForTextFile(string path)
        {
            if (!File.Exists(path))
            {
                List<string> accountInformation = new List<string>() 
                { 
                    "Knatte,123,",
                    "Fnatte,321,",
                    "Tjatte,213",
                };
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    foreach (var line in accountInformation)
                        outputFile.Write(line);
                }
            }
        }
    }
}
