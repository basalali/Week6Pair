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
        private int id = 0;

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

            cmd = new SqlCommand("SELECT * FROM category_venue cv JOIN venue ON cv.category_id = venue.id JOIN category c ON c.id = cv.category_id WHERE venue_id = @venue_id", connection);
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
            List<Category> categoryList = categorySql.GetCategories(id);

            //assert
            Assert.IsNotNull(categoryList);
            Assert.AreEqual(categoryId, categoryList.Count);
        }

        [TestMethod]
        public void Search_Category_By_ID_Test()
        {
            CategorySqlDAL getCategoryName = new CategorySqlDAL(ConnectionString);
            List<Category> searchTest = getCategoryName.GetCategories(id);

            CollectionAssert.AllItemsAreNotNull(searchTest);
            Assert.AreEqual(1, searchTest.Count);
            Equals("Tragedy Is Assured", searchTest[0].category_name);
        }
    }
}
