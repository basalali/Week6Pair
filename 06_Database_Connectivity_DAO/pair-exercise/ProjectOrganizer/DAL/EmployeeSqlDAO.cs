using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;
        private string sql_GetEmployees = "SELECT * from employee";
        private string sql_SearchEmployee = "SELECT * FROM employee WHERE first_name LIKE '%'+@firstname+'%' AND last_name LIKE '%'+@lastname+'%';";
        private string sql_GetEmployeesWithoutProjects = "SELECT * FROM employee Left JOIN project_employee ON project_employee.employee_id = employee.employee_id WHERE project_id IS NULL";

        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {

            IList<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetEmployees, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            employee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                            employee.JobTitle = Convert.ToString(reader["job_title"]);
                            employee.FirstName = Convert.ToString(reader["first_name"]);
                            employee.LastName = Convert.ToString(reader["last_name"]);
                            employee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                            employee.Gender = Convert.ToString(reader["gender"]);
                            employee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                            employees.Add(employee);

                        }

                    }

                }
            }
            catch
            {
                employees = new List<Employee>();
            }
            return employees;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            IList<Employee> searching = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_SearchEmployee, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstname", firstname);
                        cmd.Parameters.AddWithValue("@lastname", lastname);


                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Employee searcher = new Employee();
                            searcher.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            searcher.DepartmentId = Convert.ToInt32(reader["department_id"]);
                            searcher.JobTitle = Convert.ToString(reader["job_title"]);
                            searcher.FirstName = Convert.ToString(reader["first_name"]);
                            searcher.LastName = Convert.ToString(reader["last_name"]);
                            searcher.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                            searcher.Gender = Convert.ToString(reader["gender"]);
                            searcher.HireDate = Convert.ToDateTime(reader["hire_date"]);

                            searching.Add(searcher);
                        }
                        return searching;
                    }
                }
            }

            catch
            {
                searching = new List<Employee>();
            }

           return searching;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            IList<Employee> withoutProjects = new List<Employee>();
           // try
            //{
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetEmployeesWithoutProjects, conn))
                    {
                        cmd.ExecuteNonQuery();

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Employee without = new Employee();
                            without.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                            without.DepartmentId = Convert.ToInt32(reader["department_id"]);
                            without.JobTitle = Convert.ToString(reader["job_title"]);
                            without.FirstName = Convert.ToString(reader["first_name"]);
                            without.LastName = Convert.ToString(reader["last_name"]);
                            without.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                            without.Gender = Convert.ToString(reader["gender"]);
                            without.HireDate = Convert.ToDateTime(reader["hire_date"]);

                            withoutProjects.Add(without);
                        }

                    }

                }
            //}
            //catch
            //{
            //    withoutProjects = new List<Employee>();
            //}
            return withoutProjects;
        }
    }
}
