using System;
using System.Collections.Generic;

namespace DictionaryExample
{
    class Program
    {
        static void Main()
        {
            //create an empty dictionary
            Dictionary<int, string> employees = new Dictionary<int, string>()
            {
                {101, "Scott" },
                {102, "Smith" },
                {103, "Allen" }
            };

            //Remove
            employees.Remove(102);

            //foreach loop for dictionary
            foreach (KeyValuePair<int, string> item in employees)
            {
                Console.WriteLine(item.Key + ", " + item.Value);
            }

            //get value based on the key
            string s = employees[101];
            Console.WriteLine("\nValue at 101: " + s);

            //Keys
            Dictionary<int, string>.KeyCollection keys = employees.Keys;
            Console.WriteLine("\nKeys:");
            foreach (int item in keys)
            {
                Console.WriteLine(item);
            }

            //Duplicate key: exception
            //employees.Add(103, "Scott"); //Exception

            //ContainsKey
            bool a = employees.ContainsKey(103);
            Console.WriteLine("ContainsKey: " + a);

            //ContainsValue
            bool b = employees.ContainsValue("Scott");
            Console.WriteLine("ContainsValue: " + b);

            //Clear
            employees.Clear();

            Console.ReadKey();
        }
    }
}
