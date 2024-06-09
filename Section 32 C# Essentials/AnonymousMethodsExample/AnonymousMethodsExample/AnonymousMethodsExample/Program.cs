using System;

namespace AnonymousMethodsExample
{
    // Declare the delegate
    public delegate void PartyTask(string taskDetail);

    class Program
    {
        static void Main(string[] args)
        {
            // Specific friends
            Alice alice = new Alice();
            Bob bob = new Bob();
            Charlie charlie = new Charlie();

            // Create delegate instances
            PartyTask taskDelegate;

            // Assign Alice's method to the delegate
            taskDelegate = alice.SetupDecorations;
            taskDelegate("Balloons and streamers");

            // Assign Bob's method to the delegate
            taskDelegate = bob.PlayMusic;
            taskDelegate("Top hits playlist");

            // Assign Charlie's method to the delegate
            taskDelegate = charlie.ServeDrinks;
            taskDelegate("Refreshing cocktails");

            // Using an anonymous method for a quick, one-time task
            taskDelegate = delegate (string taskDetail)
            {
                Console.WriteLine($"Anonymous helper is doing: {taskDetail}");
            };
            taskDelegate("Taking group photo");

            // Multicast delegate example
            taskDelegate = alice.SetupDecorations;
            taskDelegate += bob.PlayMusic;
            taskDelegate += charlie.ServeDrinks;
            taskDelegate += delegate (string taskDetail)
            {
                Console.WriteLine($"Anonymous helper is doing: {taskDetail}");
            };

            // Call the multicast delegate
            taskDelegate("Starting the party!");

            Console.ReadKey();
        }
    }
}
