using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    //Klass som används för att skapa olika valutameny alternativ.
    public class CurrencyOptions
    {
        public string Name { get; }
        public Action<Customer> Selected { get; }

        //Konstruktorn för att skapa ett nytt valutameny alternativ.
        public CurrencyOptions(string name, Action<Customer> selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
