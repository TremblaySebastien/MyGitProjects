using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pratique
{
    class Class23
    {
        public static int DescendingOrder(int num)
        {
            int n = num.ToString().Length;

            int BMnum = num;
            int MMnum = num;
            int CMnum = num;
            int DMnum = num;
            int Mnum = num;
            int Cnum = num;
            int Dnum = num;
            int Unum = num;

            int Bmille = 0;
            int Mmille = 0;
            int Cmille = 0;
            int Dmille = 0;
            int mille = 0;
            int cent = 0;
            int dix = 0;
            int un = 0;

            int[] digits = new int[n];


            //while (BMnum > 0)
            //{
            //    BMnum -= 10000000;
            //    Bmille++;
            //}

            //int newBMille = 10000000 * (Bmille - 1);
            //while (BMnum - newBMille > 0)
            //{
            //    MMnum -= 1000000;
            //    Mmille++;
            //}

            //int newMMille = 1000000 * (Mmille - 1);
            //while (CMnum - (newBMille + newMMille) > 0)
            //{
            //    CMnum -= 100000;
            //    Cmille++;
            //}

            //int newCMille = 100000 * (Cmille - 1);
            while (DMnum /*- (/*newBMille + newMMille + newCMille*/ > 0)
            {
                DMnum -= 10000;
                Dmille++;
            }

            int newDMille = 10000 * (Dmille - 1);
            while (Mnum - (/*newBMille*/ + /*newMMille*/ + /*newCMille*/ +newDMille) > 0)
            {
                Mnum -= 1000;
                mille++;
            }

            int newMille = 1000 * (mille - 1);
            while (Cnum - (/*newBMille*/ + /*newMMille*/ + /*newCMille*/ +newDMille + newMille) > 0)
            {
                Cnum -= 100;
                cent++;
            }

            int newCent = 100 * (cent - 1);
            while (Dnum - (/*newBMille*/ + /*newMMille*/ + /*newCMille*/ +newDMille + newMille + newCent) > 0)
            {
                Dnum -= 10;
                dix++;
            }

            int newDix = 10 * (dix - 1);
            while (Unum - (/*newBMille*/ + /*newMMille*/ + /*newCMille*/ +newDMille + newMille + newCent + newDix) > 0)
            {
                Unum -= 1;
                un++;
            }

            //digits[n-8] = Bmille - 1;
            //digits[n-7] = Mmille - 1;
            //digits[n-6] = Cmille - 1;
            if (n == 5)
            {
                digits[n - 5] = Dmille - 1;
                digits[n - 4] = mille - 1;
                digits[n - 3] = cent - 1;
                digits[n - 2] = dix - 1;
                digits[n - 1] = un;
            }
            else if (n == 4)
            {
                digits[n - 4] = mille - 1;
                digits[n - 3] = cent - 1;
                digits[n - 2] = dix - 1;
                digits[n - 1] = un;
            }
            else if (n == 3)
            {
                digits[n - 3] = cent - 1;
                digits[n - 2] = dix - 1;
                digits[n - 1] = un;
            }
            else if (n == 2)
            {
                digits[n - 2] = dix - 1;
                digits[n - 1] = un;
            }
            else if (n == 1)
            {
                digits[n - 1] = un;
            }

       
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

            for (int i = 0; i < digits.Length; i++)
            {
                Console.Write(digits[i]);
            }

            return num;
        }
    }
}
