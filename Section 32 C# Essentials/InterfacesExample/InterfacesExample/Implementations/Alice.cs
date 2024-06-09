using Contracts;
using System;

namespace Implementations
{
    public class Alice : IPartyTask
    {
        public void PerformTask(string taskDetail)
        {
            Console.WriteLine($"Alice is setting up: {taskDetail}");
        }
    }
}
