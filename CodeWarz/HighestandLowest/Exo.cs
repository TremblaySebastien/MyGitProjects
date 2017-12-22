using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighestandLowest
{
    class Exo
    {
        public static string HighAndLow(string numbers)
        {
            int highest = -10000;
            int lowest = 10000;
            char delimiter = ' ';
            string[] substrings = numbers.Split(delimiter);
            
            int j;

            foreach (var substring in substrings)
            {
                if (Int32.TryParse(substring, out j))
                {
                    if (j > highest)
                    {
                        highest = j;
                    }

                    if(j < lowest)
                    {
                        lowest = j;
                    }
                }
            }
            
            numbers = highest.ToString() + " " + lowest.ToString();
            return numbers;
        }
    }
}
