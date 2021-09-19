using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2
{
    class StartMenuOption
    {
        public string Name { get;}
        public Action Selected { get;}
        public StartMenuOption(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
