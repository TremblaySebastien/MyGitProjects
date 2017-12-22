using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findoutwhethertheshapeisacube
{
    class Exo
    {
        public static bool IsCube(double volume, double side)
        {
            if(side <= 0 || volume <= 0)
            {
                return false;
            }
            else if (volume/side/side == 1)
            {
                return true;
            }
            else if (side*side*side == volume)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
