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
    public class CategorySqlDAL_Test
    {
        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";
        private int categoryId = 0;

        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd;
            connection.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM category", connection);
            categoryId = (int)cmd.ExecuteScalar();
            connection.Close();

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void GetCategoryInfo()
        {
            //arrange
            CategorySqlDAL categorySql = new CategorySqlDAL(ConnectionString);
            IList<Category> categoryList = categorySql.getCategoryInfo();

            //assert
            Assert.IsNotNull(categoryList);
            Assert.AreEqual(categoryId, categoryList.Count);
        }
    }
}
