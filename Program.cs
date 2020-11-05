using Ado.NetDemo;
using System;
namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository repository = new EmployeeRepository();
            repository.GroupingDataToFindMinMaxSumAverage();
            Console.ReadLine();
        }
    }
}
