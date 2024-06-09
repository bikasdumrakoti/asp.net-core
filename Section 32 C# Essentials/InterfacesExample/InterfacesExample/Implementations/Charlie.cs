using Contracts;
using System;

namespace Implementations
{
    public class Charlie : IPartyTask
    {
        public void PerformTask(string taskDetail)
        {
            Console.WriteLine($"Charlie is serving: {taskDetail}");
        }
    }
}
