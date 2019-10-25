using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CategorySqlDAL
    {
        private string connectionString;
        private string sql_GetCategoryContents = "SELECT * FROM category";

        public CategorySqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public List<Category> getCategoryInfo()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetCategoryContents, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.id = Convert.ToInt32(reader["id"]);
                            category.category_name = Convert.ToString(reader["name"]);

                            categories.Add(category);
                        }
                    }
                }
            }
            catch
            {
                categories = new List<Category>();
            }
            return categories;

        }
    }
}
