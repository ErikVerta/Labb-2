using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{   
    //Klass som används för att skapa olika startmeny alternativ.
    class StartMenuOption
    {
        public string Name { get;}
        public Action Selected { get;}
        
        //Konstruktorn för att skapa ett nytt startmeny alternativ.
        public StartMenuOption(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
