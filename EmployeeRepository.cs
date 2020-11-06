using Ado.NetDemo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO.NetDemo
{
    public class EmployeeRepository
    {
        //public static string ConnectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_services;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Employee;Trusted_connection=True;MultipleActiveResultSets=True";
        //public static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog =payroll_services; User ID = Ruthik; Password=Spacebar99*";
        SqlConnection connection = new SqlConnection(ConnectionString);
        public void GetAllEmployees()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
                {
                    string query = @"select * from dbo.employee_payroll";
                    SqlCommand command = new SqlCommand(query, connection);
                    //opening connection
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmployeeID = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.BasicPay = reader.GetDouble(2);
                            model.StartDate = reader.GetDateTime(3);
                            model.Gender = reader.GetString(4);
                            model.PhoneNumber = reader.GetString(5);
                            model.Address = reader.GetString(6);
                            model.Department = reader.GetString(7);
                            model.Deductions = reader.GetDouble(8);
                            model.TaxablePay = reader.GetDouble(9);
                            model.NetPay = reader.GetDouble(10);
                            model.Tax = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", model.EmployeeID, model.EmployeeName, model.Gender, model.Address, model.BasicPay, model.StartDate, model.PhoneNumber, model.Address, model.Department, model.Deductions, model.TaxablePay, model.Tax, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@Base_Pay", model.BasicPay);
                    command.Parameters.AddWithValue("@start", model.StartDate);
                    command.Parameters.AddWithValue("@gender", model.Gender);
                    command.Parameters.AddWithValue("@phone_number", model.PhoneNumber);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@department", model.Department);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@Taxable_pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Net_pay", model.NetPay);
                    command.Parameters.AddWithValue("@Income_tax", model.Tax);
                    //opening connection
                    connection.Open();
                    //Here it gives No of rows Effected
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    //Checks result if any rows effected
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Updatings the salary in data base for a employee
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int UpdateSalary(SalaryUpdateModel model)
        {
            try
            {
                int salary = 0;
                using (this.connection)
                {
                    SalaryDetailsModel displayModel = new SalaryDetailsModel();
                    SqlCommand command = new SqlCommand("dbo.spUpdateSalary", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", model.SalaryId);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", model.EmployeeId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            displayModel.EmployeeId = reader.GetInt32(0);
                            displayModel.EmployeeName = reader["EmpName"].ToString();
                            displayModel.JobDiscription = reader["JobDescription"].ToString();
                            displayModel.Month = reader["Month"].ToString();
                            displayModel.SalaryId = reader.GetInt32(4);
                            displayModel.EmployeeSalary = reader.GetInt32(5);
                            Console.WriteLine("EmployeeId={0}\nEmployeeName={1}\nEmployeeSalary={2}\nMonth={3}\nSalaryId={5}\nJobDescription={4}", displayModel.EmployeeId, displayModel.EmployeeName, displayModel.EmployeeSalary, displayModel.Month, displayModel.JobDiscription, displayModel.SalaryId);
                            Console.WriteLine("\n");
                            salary = displayModel.EmployeeSalary;

                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    return salary;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// creating method for Getting  All employee Started In given DateRange
        /// </summary>
        /// <returns></returns>
        public void GetAllemployeeStartedInADateRange()
        {
            ///creating a list to store all those employees
            EmployeeModel model = new EmployeeModel();

            using (connection)
            {
                ///passing query
                string query = "select * from employee_payroll where StartDate between cast('01-01-2018' as date) and getdate()";
                SqlCommand command = new SqlCommand(query, connection);
                ///opening connection to read
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.EmployeeID = reader.GetInt32(0);
                        model.EmployeeName = reader.GetString(1);
                        model.BasicPay = reader.GetDouble(2);
                        model.StartDate = reader.GetDateTime(3);
                        model.Gender = reader.GetString(4);
                        model.PhoneNumber = reader.GetString(5);
                        model.Address = reader.GetString(6);
                        model.Department = reader.GetString(7);
                        model.Deductions = reader.GetDouble(8);
                        model.TaxablePay = reader.GetDouble(9);
                        model.NetPay = reader.GetDouble(10);
                        model.Tax = reader.GetDouble(11);
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", model.EmployeeID, model.EmployeeName, model.Gender, model.Address, model.BasicPay, model.StartDate, model.PhoneNumber, model.Address, model.Department, model.Deductions, model.TaxablePay, model.Tax, model.NetPay);
                        Console.WriteLine("\n");
                    }
                    ///closing reader and connection
                    reader.Close();
                    connection.Close();
                }
                else
                {
                    throw new Exception("No data found");
                }
            }
        }
        /// <summary>
        /// Grouping Data To Find Min Max Sum Average
        /// UC6
        /// </summary>
        /// <returns></returns>
        public void  GroupingDataToFindMinMaxSumAverage()
        {
            EmployeeModel model = new EmployeeModel();
            using (connection)
            {
                ///query for finsing max min avg count
                string query = "select Gender,sum(BasicPay)as SUM,min(BasicPay) as MIN,max(BasicPay)as MAX,avg(BasicPay) as AVG,count(BasicPay)as COUNT from employee_payroll Group  by Gender";
                SqlCommand command = new SqlCommand(query, connection);
                ///opening connection for reading
                connection.Open();
                //executing reader 
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Gender = reader.GetString(0);
                        model.BasicPay = reader.GetDouble(1);
                        Console.WriteLine("{0},{1}", model.Gender, model.BasicPay);
                        Console.WriteLine("\n");
                    }        
                    //closing connection and reader
                    reader.Close();
                    connection.Close();
                }
                else
                {
                    throw new Exception("No data found");
                }
            }
        }
        /// <summary>
        /// UC7 Inserting  data in Multiple  tbales at once at one query
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insertingmultiple(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.SpInsertEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@Base_Pay", model.BasicPay);
                    command.Parameters.AddWithValue("@start", model.StartDate);
                    command.Parameters.AddWithValue("@gender", model.Gender);
                    command.Parameters.AddWithValue("@phone_number", model.PhoneNumber);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@department", model.Department);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@Taxable_pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Net_pay", model.NetPay);
                    command.Parameters.AddWithValue("@Income_tax", model.Tax);
                    //opening connection
                    connection.Open();
                    //Here it gives No of rows Effected
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    //Checks result if any rows effected
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
