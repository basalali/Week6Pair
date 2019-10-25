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
    public class VenueSqlDAL_Test
    {
        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";
        private int venueName = 0;

        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd;
            connection.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM venue", connection);
            venueName = (int)cmd.ExecuteScalar();
            connection.Close();

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void GetVenueInfo()
        {
            //arrange
            VenueSqlDAL venueSql = new VenueSqlDAL(ConnectionString);
            List<Venue> venuename = venueSql.GetVenueName();

            //assert
            Assert.IsNotNull(venuename);
            Assert.AreEqual(venueName, venuename.Count);
        }

    }

    
}
