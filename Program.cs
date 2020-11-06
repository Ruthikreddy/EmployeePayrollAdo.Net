using Ado.NetDemo;
using System;
namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository repository = new EmployeeRepository();
            //repository.GetAllEmployees();
            //// repository.GroupingDataToFindMinMaxSumAverage();
            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Raju";
            model.Address = "Mumbai";
            model.BasicPay = 459;
            model.Deductions = 44;
            model.Department = "Advertise";
            model.Gender = "M";
            model.PhoneNumber = "564998";
            model.NetPay = 893;
            model.Tax = 456;
            model.StartDate = DateTime.Now;
            model.TaxablePay = 459;
            Console.WriteLine(repository.Insertingmultiple(model) ? "Record inserted successfully " : "Failed");
            Console.ReadLine();
        }
    }
}
