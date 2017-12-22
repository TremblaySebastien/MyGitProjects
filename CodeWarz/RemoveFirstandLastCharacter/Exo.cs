using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveFirstandLastCharacter
{
    class Exo
    {
        public static string Remove_char(string s)
        {
            int n = s.ToString().Length;

            string newString = s;

            newString = newString.Remove(n-1, 1);
            newString = newString.Remove(0, 1);

            return newString;
        }
    }
}
