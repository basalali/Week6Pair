
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;

namespace ProjectOrgTest
{  // SCOPE_IDENTITY: Returns the last identity value inserted into an identity column in the same scope. 
    [TestClass]
    public class ProjectSqlDAOTest
    {

        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True";
        private int project_id = 0;
        private int employee_id = 0;
        private int numberOfProjects = 0;
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
                cmd = new SqlCommand("SELECT COUNT(*) FROM project", connection);
                numberOfProjects = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO employee " +
                "(department_id, first_name, last_name, job_title, birth_date, gender, hire_date) " +
                "VALUES (4, 'Nick', 'Ali', 'The Brodie', '1986-07-30', 'M', '2018-10-10');" +
                " SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                employee_id = (int)cmd.ExecuteScalar();

                //flip the last by project_ID and give us the last project; project_ID
                cmd = new SqlCommand("SELECT TOP 1 project_id FROM project_employee ORDER BY project_id DESC", connection);
                project_id = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO project_employee (project_id, employee_id) VALUES (@projectId, @employeeId);", connection);
                cmd.Parameters.AddWithValue("@projectId", project_id);
                cmd.Parameters.AddWithValue("@employeeId", employee_id);
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
        public void Get_all_projects_Test()
        {
            //Arrange

            ProjectSqlDAO allProjects = new ProjectSqlDAO(ConnectionString);

            List<Project> projectList = (List<Project>)allProjects.GetAllProjects();

            //Assert
            Assert.IsNotNull(projectList);
            Assert.AreEqual(project_id + 1, projectList.Count);

        }

        [TestMethod]
        public void Assigned_employee_to_project_by_id_Test()
        {
            int project_ids = 1;
            int employee_ids = 1;
            ProjectSqlDAO assignEm = new ProjectSqlDAO(ConnectionString);
           
            bool result = assignEm.AssignEmployeeToProject(project_ids, employee_ids);

            Assert.IsTrue(result);

        }
        [TestMethod]
        public void Remove_employee_from_project_Test()
        {
            ProjectSqlDAO removeEm = new ProjectSqlDAO(ConnectionString);

            bool result = removeEm.RemoveEmployeeFromProject(project_id, employee_id);

            Assert.IsTrue(result);
        }

    }
}