using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    {
        public static int DescendingOrder(int num)
        {


            //int[] digits = num.ToString().ToCharArray().Select(Convert.ToInt32).ToArray();


            int[] digits = { 1, 2, 3, 4, 5 };
            int n = 5; //num.ToString().Length;
            int temp = 0;
            bool swapped; 

            for (int i = 0; i < n; i++)
            {
                swapped = true;
                for (int j = 0; j < n - 1; j++)
                {
                    if (digits[j] < digits[j + 1])
                    {

                        temp = digits[j + 1];
                        digits[j + 1] = digits[j];
                        digits[j] = temp;
                        swapped = false;

                    }               
                }
                if (swapped) { break; }
            }

            for (int i = 0; i < digits.Length; i++) Console.Write(digits[i] + " ");

            //Console.WriteLine(digits[0] + ", " + digits[1] + ", " + digits[2]);

            return num;
        }


     
    }
}
