
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
            Department department = new Department();
            

            DepartmentSqlDAO gettem = new DepartmentSqlDAO(ConnectionString);

            List<Department> departmentList = new List<Department>();
                
                departmentList.GetDepartments();
            
            //Assert
            Assert.IsNotNull(departmentList);
            Assert.AreEqual(numberOfDepartments + 1, departmentList.Count);
       
        }
    }
}
