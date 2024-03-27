using RandomTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.Classes
{
    public class Dog : Animal
    {
        public sealed override void Eat()
        {
            Console.WriteLine("The dog is eating.");
        }

        public void Bark()
        {
            Console.WriteLine("The dog is barking.");
        }
    }
}
