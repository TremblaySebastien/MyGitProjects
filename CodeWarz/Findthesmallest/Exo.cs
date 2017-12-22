using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findthesmallest
{
    class Exo
    {
        public static long[] Smallest(long number)
        {
            int n = number.ToString().Length;
            long[] digits = new long[n];
            string num = number.ToString();
            //string[] numb = num.ToCharArray().Select(c => c.ToString()).ToArray();
            int lowest = -1;
            int j;
            int theLowest= 0;

            //foreach (var substring in num)
            //{
            //    if (Int32.TryParse(num, out j))
            //    {
            //        Console.WriteLine(j);
            //    }
            //}

            for (int i = 0; i < n; i++)
            {
                if (num[i] > lowest)
                {
                    Console.WriteLine(num[i]);
                    if (Int32.TryParse(num, out j))
                    {
                        Console.WriteLine(j);

                    }

                }
                //Console.WriteLine(num[i]);

            }



            //Console.WriteLine(num);
            return digits;
        }
    }
}
