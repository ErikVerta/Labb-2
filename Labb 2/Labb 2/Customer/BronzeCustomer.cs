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


        //Konstruktorn för att skapa ett nytt brons konto.
        public BronzeCustomer(string name, string password) : base(name, password)
        {
        }

        //Räknat ut totala priset för en brons kund.
        public override float CalculateTotalPrice()
        {
            var total = 0F;
            foreach (var product in Cart)
            {
                total += product.Price * CurrencyRate;
            }
            return total * Discount;
        }
    }
}
