﻿using System.Collections.Generic;
using System.Linq;
using System;

namespace LINQFirstExample
{
    class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public double Salary { get; set; }
    }

    class Program
    {
        static void Main()
        {
            //collection of objects
            List<Employee> employees = new List<Employee>()
            {
                new Employee(){EmpID=101, EmpName="Henry", Job="Designer", Salary=7232},
                new Employee(){EmpID=102, EmpName="Jack", Job="Developer", Salary=4500},
                new Employee(){EmpID=103, EmpName="Gabriel", Job="Analyst", Salary=1293},
                new Employee(){EmpID=104, EmpName="William", Job="Manager", Salary=2873},
                new Employee(){EmpID=105, EmpName="Alexa", Job="Manager", Salary=6232}
            };

            //Where
            List<Employee> filteredEmployees = employees.Where(emp => emp.Job == "Manager").ToList();
            Console.WriteLine(filteredEmployees[0].EmpID + ", " + filteredEmployees[0].EmpName + ", " + filteredEmployees[0].Job + ", " + filteredEmployees[0].Salary);

            //First
            Employee firstManager = employees.First(emp => emp.Job == "Manager");
            Console.WriteLine(firstManager.EmpID + ", " + firstManager.EmpName + ", " + firstManager.Job + ", " + firstManager.Salary);

            //FirstOrDefault
            Employee firstManager2 = employees.FirstOrDefault(emp => emp.Job == "Developer");
            if (firstManager2 != null)
            {
                Console.WriteLine(firstManager2.EmpID + ", " + firstManager2.EmpName + ", " + firstManager2.Job + ", " + firstManager2.Salary);
            }
            else
            {
                Console.WriteLine("No clerk in the list");
            }

            Console.ReadKey();
        }
    }
}
