using RandomTest.Classes;
using RandomTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomTest
{
    public class Program : IFirstInterface, ISecondInterface
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting program...");

            // Start executing DoAsyncWork asynchronously
            await DoAsyncWork();

            // While DoAsyncWork() is executing, other operations can continue

            // Perform some other operations concurrently
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Concurrent operation {i + 1}");
                await Task.Delay(1000); // Simulate some asynchronous work
            }

            Console.WriteLine("Async work completed!");
        }

        void ISecondInterface.Method()
        {
            throw new NotImplementedException();
        }

        void IFirstInterface.Method()
        {
            throw new NotImplementedException();
        }

        static async Task DoAsyncWork()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Starting async work...");
                string result = await client.GetStringAsync("https://www.example.com");
                Console.WriteLine("Async work completed!");
                // Processing the result...
            }
        }
    }
}
