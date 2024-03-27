using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest.Classes
{
    public class PublicClass
    {
        public void PublicClassMethod()
        {
            InternalClass internalClass = new InternalClass();
            internalClass.SetData(1, 2);
            internalClass.DisplayData();
        }
    }
}
