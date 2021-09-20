using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    public class Products
    {
        private static List<Products> _listOfProducts = new List<Products>();

        public static List<Products> ListofProducts
        {
            get { return _listOfProducts; }
            private set { _listOfProducts = value; }
        }
        public string Name { get; private set; }
        public float Price { get; private set; }

        //Konstruktor för att skapa nya produkter.
        public Products(string name, float price)
        {
            Name = name;
            Price = price;
            ListofProducts.Add(this);
        }

    }
}
