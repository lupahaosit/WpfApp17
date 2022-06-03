using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp17
{
    class TryCatch
    {

        public bool Sum(string sum)
        {
            try
            {
                int x = Convert.ToInt32(sum);
                return true;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"{e.Message}");
                return false;

            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
         public bool Close(int x)
        {
            int[] y = new int[Int16.MaxValue];
            try
            {
                var z = y[x];
                return true;

            }
            catch (IndexOutOfRangeException)
            {
                return false;
                
            }
        }
       
        

        
    }
}
