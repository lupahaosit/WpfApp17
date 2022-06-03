using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp17
{
    class Abonent
    {
        public string Name { get; set; }

        public int Money { get; set; }
        public bool Status { get; set; }
        public int index { get; set; }
        public string Stat { get; set; }
        public Abonent(int index, string Name, int Money)
        {
            this.index = index;
            this.Name = Name;
            this.Money = Money;
            this.Status = true;


        }
        public  void SendMoney(int x, Abonent y)
        {
            if (Convert.ToInt32(this.Money) >= x)
            {
                this.Money -= x;
                y.Money += x;
            }
            else
            {
                Console.WriteLine(" not enough money");
            }
        }
        public void SendMoneyND(int x, NonDeposite y)
        {
            if (x < y.Money)
            {
                y.Money += x;
                this.Money -= x;
            }

        }
        public void SendMoneyD(int x, Deposite y)
        {
            if (x < y.Money)
            {
                y.Money += x;
                this.Money -= x;
            }
        }


    }
}
