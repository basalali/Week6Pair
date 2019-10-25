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
        private string sql_GetCategoryById = "SELECT * FROM category_venue cv JOIN venue ON cv.category_id = venue.id JOIN category c ON c.id = cv.category_id WHERE venue_id = @venue_id";

        public CategorySqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        
        private Category ConvertReaderToCategory(SqlDataReader reader)
        {
            Category cat = new Category();

            cat.category_name = Convert.ToString(reader["name"]);
            cat.id = Convert.ToInt32(reader["id"]);

            return cat;
        }

        public Category GetCategories(int id)
        {
            Category cat = new Category();
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
                            cat = result;

                        }

                    }
                    return cat;
                }
            }

            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                cat = new Category();
            }

            return cat;
        }


    }
}
