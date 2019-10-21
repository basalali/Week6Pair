using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private string connectionString;
        private string sql_GetDepartmentById = "SELECT department_id, name FROM department ";
        private string sql_NewDepartment = "INSERT INTO department (name) VALUES(@Name)";
        private string sql_UpdateDepartment = " UPDATE department SET name = @Name WHERE department_id = @id";

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            IList<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetDepartmentById, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Department department = new Department();
                            department.Id = Convert.ToInt32(reader["department_id"]);
                            department.Name = Convert.ToString(reader["Name"]);

                            departments.Add(department);

                        }

                    }

                }
            }
            catch
            {
                departments = new List<Department>();
            }
            return departments;
        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_NewDepartment, conn))
                    {
                       
                        cmd.Parameters.AddWithValue("@Name", newDepartment.Name);

                        result = cmd.ExecuteNonQuery();
                       
                    }
                }
            }
            catch(SqlException)
            {
                return result;
            }
            return result;
        }




        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {


            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))

                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_UpdateDepartment, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", updatedDepartment.Id);
                        cmd.Parameters.AddWithValue("@Name", updatedDepartment.Name);

                        int count = cmd.ExecuteNonQuery();

                        if (count == 1)
                        {
                            return true;
                        }

                    }

                }


            }
            catch
            {
                throw;
            }
            return result;

        }

    }
}

