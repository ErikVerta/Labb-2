using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    class BronzeCustomer : Customer
    {
        private static float _discount = 0.95F;

        private static float Discount
        {
            get { return _discount; }
        }


        public BronzeCustomer(string name, string password) : base(name, password)
        {
        }
        public override float CalculateTotalPrice()
        {
            var total = 0F;
            foreach (var item in Cart)
            {
                total += item.PriceOfProduct * CurrencyRate;
            }
            return total * Discount;
        }
    }
}
