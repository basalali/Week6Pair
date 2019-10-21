using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;
        private string sql_GetAllProjects = "SELECT * FROM project";
        private string sql_AssignEmployee = "INSERT INTO project_employee (project_id, employee_id) VALUES (@projectId, @employeeId)";
        private string sql_RemoveEmployee = "DELETE FROM project_employee WHERE project_id = @projectId AND employee_Id = @employeeId";
        private string sql_CreateProject = "INSERT INTO project VALUES (@Name, @StartDate, @EndDate)";



        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            IList<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql_GetAllProjects, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Project project = new Project();
                            project.ProjectId = Convert.ToInt32(reader["project_id"]);
                            project.Name = Convert.ToString(reader["name"]);
                            project.StartDate = Convert.ToDateTime(reader["from_date"]);
                            project.EndDate = Convert.ToDateTime(reader["to_date"]);

                            projects.Add(project);
                        }

                    }

                }
            }
            catch
            {
                projects = new List<Project>();
            }
            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {          
                bool result = false;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))

                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql_AssignEmployee, conn))
                        {
                        cmd.Parameters.AddWithValue("@projectId", projectId);
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);

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

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))

                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_RemoveEmployee, conn))
                    {
                        cmd.Parameters.AddWithValue("@projectId", projectId);
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);

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

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql_CreateProject, conn))
                    {

                        cmd.Parameters.AddWithValue("@Name", newProject.Name);
                        cmd.Parameters.AddWithValue("@StartDate", newProject.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", newProject.EndDate);
                     

                        result = cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException)
            {
                return result;
            }
            return result;
        }

    }
}
