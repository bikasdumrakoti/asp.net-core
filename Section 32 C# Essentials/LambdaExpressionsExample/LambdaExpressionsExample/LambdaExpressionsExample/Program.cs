using System;

namespace LambdaExpressionsExample
{
    // Declare the delegate
    public delegate void PartyTask(string taskDetail);

    public class Program
    {
        static void Main(string[] args)
        {
            // Specific friends
            Alice alice = new Alice();
            Bob bob = new Bob();
            Charlie charlie = new Charlie();

            // Using lambda expressions
            PartyTask taskDelegate;

            // Setup decorations with lambda expression
            taskDelegate = (taskDetail) => Console.WriteLine($"Lambda helper is setting up: {taskDetail}");
            taskDelegate("Balloons and streamers");

            // Play music with lambda expression
            taskDelegate = (taskDetail) => Console.WriteLine($"Lambda helper is playing music: {taskDetail}");
            taskDelegate("Top hits playlist");

            // Serve drinks with lambda expression
            taskDelegate = (taskDetail) => Console.WriteLine($"Lambda helper is serving drinks: {taskDetail}");
            taskDelegate("Refreshing cocktails");

            // Multicast delegate with lambda expressions
            taskDelegate = (taskDetail) => Console.WriteLine($"Lambda helper is setting up: {taskDetail}");
            taskDelegate += (taskDetail) => Console.WriteLine($"Lambda helper is playing music: {taskDetail}");
            taskDelegate += (taskDetail) => Console.WriteLine($"Lambda helper is serving drinks: {taskDetail}");

            // Call the multicast delegate
            taskDelegate("Starting the party!");

            Console.ReadKey();
        }
    }
}
