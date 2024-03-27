using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.Classes
{
    internal class InternalClass
    {
        int real;
        int img;

        public void SetData(int r, int i)
        {
            real = r;
            img = i;
        }

        public void DisplayData()
        {
            Console.WriteLine("Real = {0}", real);
            Console.WriteLine("Imaginary = {0}", img);
        }
    }
}
