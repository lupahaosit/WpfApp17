using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp17
{
    class NonDeposite: Abonent
    {
        Random rnd = new Random();
        public NonDeposite(int index, string Name, int Money)
            : base(index, Name, Money)
        {
          
           
        }
        
    }
}
