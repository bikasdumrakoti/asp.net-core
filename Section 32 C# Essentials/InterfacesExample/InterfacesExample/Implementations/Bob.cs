using Contracts;
using System;

namespace Implementations
{
    public class Bob : IPartyTask
    {
        public void PerformTask(string taskDetail)
        {
            Console.WriteLine($"Bob is playing: {taskDetail}");
        }
    }
}
