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

        public static List<CurrencyOptions> CurrencyOptions { get; set; }

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
                new StartMenuOption("Exit", () => ExitProgram()),
            };

            MainMenuOptions = new List<MainMenuOption> 
            {
                new MainMenuOption("Shop", (customer) => customer.Shop()),
                new MainMenuOption("Go to Cart", (customer) => customer.ToString()),
                new MainMenuOption("Go to Checkout", (customer) => customer.Checkout()),
                new MainMenuOption("Change currency", (customer) => CurrencyMenu(customer)),
                new MainMenuOption("Logout", (customer) => StartMenu()), 
                new MainMenuOption("Exit", (customer) => ExitProgram()),
            };

            CurrencyOptions = new List<CurrencyOptions>
            {
                new CurrencyOptions("Change to SEK",(customer) => customer.CurrencyConvertToSEK()),
                new CurrencyOptions("Change to USD",(customer) => customer.CurrencyConvertToDollar()),
                new CurrencyOptions("Change to Pounds", (customer) => customer.CurrencyConvertToPounds()),
            };
           
            StartMenu();
        }
        
        //Första skärmen där användaren får välja att logga in eller registera ett nytt kundkonto.
        public static void StartMenu()
        {
            int selectorPosition = 0;

            PrintStartMenu(StartMenuOptions, StartMenuOptions[selectorPosition]);
     
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectorPosition + 1 < StartMenuOptions.Count)
                    {
                        selectorPosition++;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectorPosition - 1 >= 0)
                    {
                        selectorPosition--;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    StartMenuOptions[selectorPosition].Selected.Invoke();
                }               
            } while (true);
        }
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
        //Huvudmenyn där kunden kan välja att shoppa, se kundvagn, betala eller logga ut.
        public static void MainMenu(Customer customer)
        {
            int selectorPosition = 0;

            PrintMainMenu(MainMenuOptions, MainMenuOptions[selectorPosition]);
         
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectorPosition + 1 < MainMenuOptions.Count)
                    {
                        selectorPosition++;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectorPosition - 1 >= 0)
                    {
                        selectorPosition--;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    MainMenuOptions[selectorPosition].Selected.Invoke(customer);
                }

            } while (true);
        }
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
        public static void CurrencyMenu(Customer customer)
        {
            int selectorPosition = 0;

            PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[selectorPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectorPosition + 1 < CurrencyOptions.Count)
                    {
                        selectorPosition++;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectorPosition - 1 >= 0)
                    {
                        selectorPosition--;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[selectorPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    CurrencyOptions[selectorPosition].Selected.Invoke(customer);
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    MainMenu(customer);
                }
            } while (true);
        }
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
        private static void ExitProgram()
        {
            WriteTextfile();
            Environment.Exit(-1);
        }
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

        private static void ReadTextfile()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string path = Path.Combine(docPath, "Labb2AccountInfo.txt");

            CheckForStandardAccounts(path);

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
        private static void CheckForStandardAccounts(string path)
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
