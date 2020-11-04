using Ado.NetDemo;
using System;
namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository salary = new EmployeeRepository();
            SalaryUpdateModel updateModel = new SalaryUpdateModel()
            {
                SalaryId = 1,
                Month = "Jan",
                EmployeeSalary = 120,
                EmployeeId = 1
            };
            int EmpSalary = salary.UpdateSalary(updateModel);
            //Console.WriteLine("return int {0}",salary.UpdateSalary(updateModel));
            Console.ReadLine();
        }
    }
}
