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
        private string sql_GetDepartmentById = "SELECT * FROM department WHERE department_id = @department_id";
        private string sql_NewDepartment = "INSERT.INTO department (department_id, name), VALUES(department_id, name)"
        private string sql_UpdateDepartment = " UPDATE department SET id = ???, name = ???";

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

                        cmd.Parameters.AddWithValue("@department_id", department_id);

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
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_NewDepartment, conn))
                    {
                        cmd.Parameters.AddWithValue("@department_id", newDepartment.Id);
                        cmd.Parameters.AddWithValue("@Name", newDepartment.Name);

                        int count = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                return "";
            }
            return newDepartment.Id;
        }


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
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);
                    cmd.Parameters.AddWithValue("@Name", updatedDepartment.Name);

                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        return true;
                    }

                }

            }


        }
        catch
        {
            return false;
        }
        return result;

    }

} 

