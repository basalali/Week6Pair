
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;

namespace ProjectOrgTest
{
    [TestClass]
    public class ProjectSqlDAOTest
    {
        protected string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void Remove_Employee_From_Project()
        {
            // Arrange
            ProjectSqlDAO doa = new ProjectSqlDAO(ConnectionString);

            Project project = new Project();
            project.Name = "Tech Elevator";


            doa.AssignEmployeeToProject(1, 6);

            int beforeDeleteCount = GetAllProjects("project");

            //ACTION
            bool result = doa.RemoveEmployeeFromProject(1,6);
            int afterDeleteCount = GetRowCount("project");

            // ASSERT
            Assert.IsTrue(result, "RemoveEmployee returned false");
            Assert.AreEqual(beforeDeleteCount - 1, afterDeleteCount, "Row count did not decrease.");


        }
 


    }
}