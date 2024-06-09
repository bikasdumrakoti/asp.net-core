using Contracts;
using Implementations;
using System;

namespace InterfacesExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create instances of friends
            IPartyTask alice = new Alice();
            IPartyTask bob = new Bob();
            IPartyTask charlie = new Charlie();

            // Perform tasks using the interface
            alice.PerformTask("Balloons and streamers");
            bob.PerformTask("Top hits playlist");
            charlie.PerformTask("Refreshing cocktails");

            // Can also store them in a collection
            IPartyTask[] partyHelpers = { alice, bob, charlie };

            // Iterate over each helper and perform their task
            foreach (var helper in partyHelpers)
            {
                helper.PerformTask("Starting the party!");
            }

            Console.ReadKey();
        }
    }
}
