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
        private string sql_GetCategoryById = "SELECT c.name FROM category_venue cv JOIN venue ON cv.venue_id = venue.id JOIN category c ON c.id = cv.category_id WHERE venue_id = @venue_id";

        public CategorySqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        
        private Category ConvertReaderToCategory(SqlDataReader reader)
        {
            Category cat = new Category();

            cat.category_name = Convert.ToString(reader["name"]);

            return cat;
        }

        public List<Category> GetCategories(int id)
        {
            List<Category> cat = new List<Category>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetCategoryById, conn))
                    {
                        cmd.Parameters.AddWithValue("@venue_id", id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Category result = ConvertReaderToCategory(reader);
                            cat.Add(result);

                        }

                    }
                    return cat;
                }
            }

            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                cat = new List<Category>();
            }

            return cat;
        }


    }
}
