using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;

namespace ProjectOrgTest
{
    [TestClass]
    public class EmployeeSqlDAOTest
    {

        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private int numberOfEmployees = 0;
        private int employeeID = 0;
        private int projectID = 0;
        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;


        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd;
                connection.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM employee", connection);
                numberOfEmployees = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand(
                            "INSERT INTO employee " +
                            "(department_id, first_name, last_name, job_title, birth_date, gender, hire_date) " +
                            "VALUES (4, 'TheFun', 'Employee', 'Example', '2019-10-10', 'M', '1234-1-2');" +
                            " SELECT CAST(SCOPE_IDENTITY() as int);",
                            connection);
                employeeID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) " +"FROM employee " + "LEFT JOIN project_employee ON project_employee.employee_id = employee.employee_id " +"WHERE project_employee.project_id IS NULL",connection);
                projectID= (int)cmd.ExecuteScalar();
            }

        }



        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }


        [TestMethod]
        public void Get_Employee_Test()
        {
            //Arrange

            EmployeeSqlDAO getEmployee = new EmployeeSqlDAO(ConnectionString);

            List<Employee> employeeList = (List<Employee>)getEmployee.GetAllEmployees();

            //Assert
            Assert.IsNotNull(employeeList);
            Assert.AreEqual(numberOfEmployees + 1, employeeList.Count);

        }

        [TestMethod]
        public void Search_Test()
        {
            EmployeeSqlDAO getEmployee = new EmployeeSqlDAO(ConnectionString);
            List<Employee> searchTest = (List<Employee>)getEmployee.Search("TheFun", "Employee");

            CollectionAssert.AllItemsAreNotNull(searchTest);
            Assert.AreEqual(1, searchTest.Count);
            Equals("TheFun", searchTest[0].FirstName);
        }
        [TestMethod]
        public void Get_employees_not_assigned_to_tests()
        {
            EmployeeSqlDAO employeesNotAssigned = new EmployeeSqlDAO(ConnectionString);
            List<Employee> noProjectList = (List<Employee>)employeesNotAssigned.GetEmployeesWithoutProjects();

            Assert.AreEqual(projectID, noProjectList.Count);
        }

    }

}