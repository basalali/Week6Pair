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
            List<Category> categoryList = categorySql.getCategoryInfo();

            //assert
            Assert.IsNotNull(categoryList);
            Assert.AreEqual(categoryId, categoryList.Count);
        }

        [TestMethod]
        public void Search_Category_By_Venue_ID_Test()
        {
            CategorySqlDAL getCategoryName = new CategorySqlDAL(ConnectionString);
            List<Category> searchTest = getCategoryName.getCategoryInfo();

            CollectionAssert.AllItemsAreNotNull(searchTest);
            Assert.AreEqual(1, searchTest.Count);
            Equals("Tragedy Is Assured", searchTest[0].category_name);
        }
    }
}
