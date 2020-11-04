using System;
namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
             EmployeeRepository repository = new EmployeeRepository();
            // repository.GetAllEmployees();
            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Axar";
            model.Address = "Waranagl";
            model.BasicPay = 459;
            model.Deductions = 44;
            model.Department = "HR";
            model.Gender = "M";
            model.PhoneNumber = "564998";
            model.NetPay = 893;
            model.Tax = 456;
            model.StartDate = DateTime.Now;
            model.TaxablePay = 459;
            //ternary Operator for Adding Employee or not
            Console.WriteLine(repository.AddEmployee(model) ? "Record inserted successfully " : "Failed");
            Console.ReadLine();
        }
    }
}
