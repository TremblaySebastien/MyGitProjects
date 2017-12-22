using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenOrOdd
{
    class Exo
    {
        

        public static string EvenOrOdd(int number)
        {
            int n;
            string result;

            n = number;

            if(n % 2 == 0)
            {
                result = "Even";
            }
            else
            {
                result = "Odd";
            }

            return result;
        }
        
    }
}
