using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.Classes
{
    public class Animal
    {
        public string Species { get; set; }

        public virtual void Eat()
        {
            Console.WriteLine("The animal is eating.");
        }
    }
}
