using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    class GoldCustomer : Customer
    {
        private static float _discount = 0.85F;

        private static float Discount
        {
            get { return _discount; }          
        }


        //Konstruktorn för att skapa ett nytt guld konto.
        public GoldCustomer(string type, string name, string password) : base(type, name, password) 
        {
        }
        
        //Räknat ut totala priset för en guld kund.
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
