
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
    public class DepartmentSqlDAOTest
    {

        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private int numberOfDepartments = 0;
        private int departmentID = 0;
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
                cmd = new SqlCommand("SELECT COUNT(*) FROM department", connection);
                numberOfDepartments = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO department (name) VALUES ('TestDepartment'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                departmentID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("UPDATE department SET name = 'TestDepartment2' WHERE name = 'TestDepartment'", connection);
                cmd.ExecuteNonQuery();
            }

        }



        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }


        [TestMethod]
        public void GetDepartmentTest()
        {
            //Arrange

            DepartmentSqlDAO gettem = new DepartmentSqlDAO(ConnectionString);

            List<Department> departmentList = (List<Department>)gettem.GetDepartments();

            //Assert
            Assert.IsNotNull(departmentList);
            Assert.AreEqual(numberOfDepartments + 1, departmentList.Count);

        }
        [TestMethod]
        public void Create_Dept_Test()
        {
            DepartmentSqlDAO testClass = new DepartmentSqlDAO(ConnectionString);

            Department createDepartTest = new Department();
            createDepartTest.Name = "TestCreate";         
            int isItWorking = testClass.CreateDepartment(createDepartTest);
            List<Department> deptList = (List<Department>)testClass.GetDepartments();

            Assert.AreEqual(1, isItWorking);
            Assert.AreEqual(numberOfDepartments + 2, deptList.Count);

        }

        [TestMethod]
        public void Update_Department_Test()
        {
            DepartmentSqlDAO testClass = new DepartmentSqlDAO(ConnectionString);
            Department testDept = new Department();

            testDept.Name = "TechElevator";
            testDept.Id = departmentID;
            bool isItWorking = testClass.UpdateDepartment(testDept);

            Assert.IsTrue(isItWorking);
        }
    }
}   
