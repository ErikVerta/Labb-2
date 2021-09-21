using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labb_2
{   
    class Program
    {

        private static int PointerPosition { get; set; }
        private static List<StartMenuOption> StartMenuOptions { get; set; }

        private static List<MainMenuOption> MainMenuOptions { get; set; }

        private static List<CurrencyOptions> CurrencyOptions { get; set; }

        private static List<string> CustomerTypes { get; set; }

        //Instansierar alla nödvändiga objekt.
        static void Main(string[] args)
        {
            ReadTextfile();

            new Products("Apple", 3.5F);
            new Products("Hotdog", 5F);
            new Products("Soda", 10);

            CustomerTypes = new List<string> {"Gold", "Silver","Bronze","Basic" };

            StartMenuOptions = new List<StartMenuOption>
            {
                new StartMenuOption("Login", () => Customer.Login()),
                new StartMenuOption("Create new account", () => CreateAccount()),
            };

            MainMenuOptions = new List<MainMenuOption>
            {
                new MainMenuOption("Shop", (customer) => customer.Shop()),
                new MainMenuOption("Go to Cart", (customer) => customer.ShowCart()),
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
            PointerPosition = 0;

            PrintStartMenu(StartMenuOptions, StartMenuOptions[PointerPosition]);
     
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (PointerPosition + 1 < StartMenuOptions.Count)
                    {
                        PointerPosition++;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (PointerPosition - 1 >= 0)
                    {
                        PointerPosition--;
                        PrintStartMenu(StartMenuOptions, StartMenuOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    StartMenuOptions[PointerPosition].Selected.Invoke();
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
            PointerPosition = 0;

            PrintMainMenu(MainMenuOptions, MainMenuOptions[PointerPosition]);
         
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (PointerPosition + 1 < MainMenuOptions.Count)
                    {
                        PointerPosition++;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (PointerPosition - 1 >= 0)
                    {
                        PointerPosition--;
                        PrintMainMenu(MainMenuOptions, MainMenuOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {                   
                    MainMenuOptions[PointerPosition].Selected.Invoke(customer);
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
            PointerPosition = 0;

            PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[PointerPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (PointerPosition + 1 < CurrencyOptions.Count)
                    {
                        PointerPosition++;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (PointerPosition - 1 >= 0)
                    {
                        PointerPosition--;
                        PrintCurrencyMenu(CurrencyOptions, CurrencyOptions[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    CurrencyOptions[PointerPosition].Selected.Invoke(customer);
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

            string choiseOfCustomerType = ChooseCustomerTypeMenu();

            WriteTextfile(choiseOfCustomerType, inputName, inputPassword);
            ReadTextfile();
            Console.WriteLine("Account created.");            
            System.Threading.Thread.Sleep(2000);
            StartMenu();
        }

        private static string ChooseCustomerTypeMenu()
        {
            PointerPosition = 0;

            PrintCustomerTypeMenu(CustomerTypes, CustomerTypes[PointerPosition]);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (PointerPosition + 1 < CustomerTypes.Count)
                    {
                        PointerPosition++;
                        PrintCustomerTypeMenu(CustomerTypes, CustomerTypes[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (PointerPosition - 1 >= 0)
                    {
                        PointerPosition--;
                        PrintCustomerTypeMenu(CustomerTypes, CustomerTypes[PointerPosition]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return CustomerTypes[PointerPosition];
                }

            } while (true);
        }

        private static void PrintCustomerTypeMenu(List<string> options, string selectedOption)
        {
            Console.Clear();
            Console.WriteLine("Choose a customer type:");
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
                Console.WriteLine(option);
            }
            Console.WriteLine("Press <UpArrow> or <DownArrow> to navigate. Press <Enter> to select or press <Backspace> to go to Main Menu.");
        }
        
        //Skriver namn och lösenord för alla konton till en .txt fil.
        private static void WriteTextfile(string type, string name, string password)
        {
            List<string> accountInformation = new List<string>();
            foreach (var customer in Customer.CustomerList)
            {
                accountInformation.Add($"{customer.CustomerType},{customer.Name},{customer.Password},");
            }
            accountInformation.Add($"{type},{name},{password},");

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
               
                
                List<string> accountInfo = text.Split(',').ToList<string>();
                accountInfo.Remove("");
                for (int i = 0; i < accountInfo.Count(); i += 3)
                {
                    if (accountInfo[i] == "Gold")
                    {
                        new GoldCustomer(accountInfo[i + 1], accountInfo[i + 2], accountInfo[i]);
                    }
                    else if (accountInfo[i] == "Silver")
                    {
                        new SilverCustomer(accountInfo[i + 1], accountInfo[i + 2], accountInfo[i]);
                    }
                    else if (accountInfo[i] == "Bronze")
                    {
                        new BronzeCustomer(accountInfo[i + 1], accountInfo[i + 2], accountInfo[i]);
                    }
                    else if(accountInfo[i] == "Basic")
                    {
                        new Customer(accountInfo[i + 1], accountInfo[i + 2], accountInfo[i]);
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
                    "Gold,Knatte,123,",
                    "Silver,Fnatte,321,",
                    "Bronze,Tjatte,213",
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
