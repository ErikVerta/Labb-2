using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    public class CurrencyOptions
    {
        public string Name { get; }
        public Action<Customer> Selected { get; }
        public CurrencyOptions(string name, Action<Customer> selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
