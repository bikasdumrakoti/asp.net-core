using Company;
using System;

namespace ManyToOneExample
{
    class Program
    {
        static void Main()
        {
            //Three employees in same department
            Employee employee1 = new Employee() { EmployeeID = 1, EmployeeName = "Scott", Email = "scott@gmail.com" };
            Employee employee2 = new Employee() { EmployeeID = 2, EmployeeName = "Allen", Email = "allen@gmail.com" };
            Employee employee3 = new Employee() { EmployeeID = 3, EmployeeName = "Smith", Email = "smith@gmail.com" };

            //create object of Department class
            Department department = new Department() { DepartmentID = 10, DepartmentName = "Accounting" };
            employee1.dept = department;
            employee2.dept = department;
            employee3.dept = department;

            //print
            Console.WriteLine("FIRST EMPLOYEE:");
            Console.WriteLine("Employee ID: " + employee1.EmployeeID);
            Console.WriteLine("Employee Name: " + employee1.EmployeeName);
            Console.WriteLine("Email: " + employee1.Email);
            Console.WriteLine("Department ID: " + employee1.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee1.dept.DepartmentName);

            Console.WriteLine("\nSECOND EMPLOYEE:");
            Console.WriteLine("Employee ID: " + employee2.EmployeeID);
            Console.WriteLine("Employee Name: " + employee2.EmployeeName);
            Console.WriteLine("Email: " + employee2.Email);
            Console.WriteLine("Department ID: " + employee2.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee2.dept.DepartmentName);

            Console.WriteLine("\nTHIRD EMPLOYEE:");
            Console.WriteLine("Employee ID: " + employee3.EmployeeID);
            Console.WriteLine("Employee Name: " + employee3.EmployeeName);
            Console.WriteLine("Email: " + employee3.Email);
            Console.WriteLine("Department ID: " + employee3.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee3.dept.DepartmentName);

            Console.ReadKey();
        }
    }
}
