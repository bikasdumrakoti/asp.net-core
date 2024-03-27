using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.Classes
{
    public class Labrador : Dog
    {
        public void Swim()
        {
            Console.WriteLine("The Labrador is swimming.");
            Console.WriteLine($"{Species}");
            Eat();
            Bark();
        }
    }
}
