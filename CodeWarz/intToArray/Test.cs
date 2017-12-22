using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intToArray
{
    class Test
    {
        public static int DescendingOrder(int num)
        {
            int n = num.ToString().Length;
            int nCount = 0;

            int v0 = num;
            int v1;
            int v2;
            int v3;
            int step1 = 100000000;

            int[] digits = new int[n];

            for(int i = 0; i < n; i++)
            {
            
                v1 = num % step1;
                v2 = (num - v1) / step1;
             

                //v3 = (num - v2) / step1;
                //v3 = v2 - v1;
                //v2 = (num - v1) / step1;
                //v3 = v1 - v2;

                step1 /= 10;
                digits[n - (n - i)] = v2;
                //Console.WriteLine(v3);
                Console.WriteLine("v1= " + v1);
                Console.WriteLine("v2= " + v2);
                //Console.WriteLine("v3= " + v3);
                Console.WriteLine("num= " + num);

                //Console.WriteLine(v3);
            }


            //65687
            //digits[n - (n - i)]
            //int[] digits = { 5, 6, 8, 7 };

            //int valeuralindex;

            //for (int i = 0; i < n; i++)
            //{

            //    valeuralindex = digits[n - (n - i)];
            //    Console.WriteLine(valeuralindex);
            //}



            return num;
        }
    }
}
