using System;

namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
             EmployeeRepository repository = new EmployeeRepository();
             repository.GetAllEmployees();
            Console.ReadLine();
        }
    }
}
