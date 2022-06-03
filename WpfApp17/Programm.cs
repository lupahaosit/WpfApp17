using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp17
{
    class Programm
    {

        Random rnd = new Random();
        public List<Deposite> Deposites()
        {
            List<Deposite> ab = new List<Deposite>();
            for (int i = 0; i < 10; i++)
            {
                string x = Guid.NewGuid().ToString().Substring(0, 5);
                int y = rnd.Next(1000, 100000);
                ab.Add(new Deposite(i, x, y));
            }
            return ab;
        }
        public List<NonDeposite> NonDeposites()
        {
            List<NonDeposite> ab = new List<NonDeposite>();
            for (int i = 0; i < 10; i++)
            {
                string x = Guid.NewGuid().ToString().Substring(0, 5);
                int y = rnd.Next(1000, 100000);
                ab.Add(new NonDeposite(i, x, y));
            }
            return ab;
        }
    }
}
