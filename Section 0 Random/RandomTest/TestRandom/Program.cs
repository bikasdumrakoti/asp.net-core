using System;

namespace TestRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            InternalClass internalClass = new InternalClass();
            internalClass.SetData(3, 4);
            internalClass.DisplayData();
        }
    }
}
