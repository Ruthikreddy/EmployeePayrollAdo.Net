using Ado.NetDemo;
using ADO.NetDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SqlTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenSalaryDetails_AbleToUpdateSalarayDetails()
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

            Assert.AreEqual(updateModel.EmployeeSalary, EmpSalary);
        }
    }
}