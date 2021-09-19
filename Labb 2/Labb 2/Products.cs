using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    public class Products
    {
        private static List<Products> _assortment = new List<Products>();

        public static List<Products> Assortment
        {
            get { return _assortment; }
            set { _assortment = value; }
        }
        public string NameOfProduct { get; set; }
        public float PriceOfProduct { get; private set; }

        //Konstruktor för att skapa nya produkter.
        public Products(string name, float price)
        {
            NameOfProduct = name;
            PriceOfProduct = price;
            Assortment.Add(this);
        }

    }
}
