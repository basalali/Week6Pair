using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class GetAllSpacesTest
    {
        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";
        int id = 0;
        private TransactionScope transaction;

       [TestInitialize]
       public void Setup()
        {
            transaction = new TransactionScope();

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd;
                connection.Open();
                cmd = new SqlCommand("SELECT * FROM space", connection);
                connection.Close();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void Get_All_Spaces_Test()
        {
            SpaceSqlDAL getAllSpaces = new SpaceSqlDAL(ConnectionString);

            List<Space> spaceList = (List<Space>)getAllSpaces.GetSpaceDetails(id);

            Assert.IsNotNull(spaceList);

        }

    }
}
