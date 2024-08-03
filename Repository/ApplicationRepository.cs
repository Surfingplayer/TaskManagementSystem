using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repository
{
    public class ApplicationRepository
    {
         private readonly string _connectionString;

        public ApplicationRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionStringTMS");
        }
        public Employee Login(Employee user)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_Login", connection);
                        command.CommandType=System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", user.EmpEmail);
                        command.Parameters.AddWithValue("@Password", user.EmpPassword);
                        Employee employee = new Employee();
                    
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            employee.EmpID= (int)reader["empId"];
                            employee.EmpName = reader["empName"].ToString();
                            employee.EmpEmail = reader["empEmail"].ToString();
                            employee.EmpPassword = reader["EmpPassword"].ToString();
                            employee.IsActive = (bool)reader["isActive"];
                        }
                        return employee;
                    }
                

                }catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public IEnumerable<Client> ClientList()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_Clients", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        List<Client> clientList = new List<Client>();
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Client client = new Client();
                            client.ClientID = (int)reader["clientID"];
                            client.ClientName = reader["clientName"].ToString();
                            client.IsActive = (bool)reader["isActive"];

                            clientList.Add(client);
                        }
                        return clientList;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public IEnumerable<Project> ProjectList(int clientId)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_ClientProjects", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientId", clientId);

                        List<Project> clientList = new List<Project>();
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Project project = new Project();
                            project.ProjectID = (int)reader["projectID"];
                            project.ProjectName = reader["projectName"].ToString();
                            project.ClientID = (int)reader["clientID"];
                            project.IsActive = (bool)reader["isActive"];

                            clientList.Add(project);
                        }
                        return clientList;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public IEnumerable<DataBase> DataBaseList(int projectId)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_ProjectDataBase", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        List<DataBase> clientList = new List<DataBase>();
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DataBase database = new DataBase();
                            database.ProjectID = (int)reader["projectID"];
                            database.DataBaseName = reader["dataBaseName"].ToString();
                            database.DataBaseID = (int)reader["dataBaseID"];
                            database.IsActive = (bool)reader["isActive"];

                            clientList.Add(database);
                        }
                        return clientList;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public IEnumerable<Category> CategoryList()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_Categories", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        List<Category> clientList = new List<Category>();
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.CategoryID = (int)reader["categoryID"];
                            category.CategoryName = reader["categoryName"].ToString();
                            category.IsActive = (bool)reader["isActive"];

                            clientList.Add(category);
                        }
                        return clientList;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public int InsertTask(TaskManagementSystem.Models.Task task)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SPI_Task", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@dataBaseID", task.DataBaseID);
                    command.Parameters.AddWithValue("@categoryID", task.CategoryID);
                    command.Parameters.AddWithValue("@taskTitle", task.TaskTitle);
                    command.Parameters.AddWithValue("@taskDescription", task.TaskDescription);
                    command.Parameters.AddWithValue("@assignedBy", task.AssignedBy);
                    command.Parameters.AddWithValue("@empID", task.EmpID);



                        SqlParameter success = new SqlParameter
                        {
                            ParameterName = "@success",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(success);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@success"].Value;
                }
            }
            
            catch (Exception ex)
            {
                return 0;
            }
        }


        public IEnumerable<TaskManagementSystem.Models.Task> ListTasks() 
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SPR_Task", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
        

                    List<TaskManagementSystem.Models.Task> taskList = new List<TaskManagementSystem.Models.Task>();
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TaskManagementSystem.Models.Task task = new TaskManagementSystem.Models.Task();
                        task.TaskID = (int)reader["taskID"];
                        task.TaskTitle = reader["taskTilte"].ToString();
                        task.DataBaseID = (int)reader["dataBaseID"];
                        task.EmpID= (int)reader["empID"];
                        task.TaskDescription= reader["taskDescription"].ToString();
                        task.IsActive = (bool)reader["isActive"];
                        task.AssignedBy = reader["assignedby"].ToString();
                        task.CategoryID = (int)reader["CategoryID"];
                        taskList.Add(task);
                    }
                    return taskList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
    }

        public IEnumerable<TaskManagementSystem.ViewModel.TaskInfoViewModel> ListTasksNotes(int taskId)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SPR_TaskNotes", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TaskId", taskId);
                        List<TaskManagementSystem.ViewModel.TaskInfoViewModel> taskNotesList = new List<TaskManagementSystem.ViewModel.TaskInfoViewModel>();
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            TaskManagementSystem.ViewModel.TaskInfoViewModel taskNotes = new TaskManagementSystem.ViewModel.TaskInfoViewModel();
                            taskNotes.TaskID = (int)reader["taskID"];
                            taskNotes.EmpID = (int)reader["empID"];
                            taskNotes.Notes = reader["notes"].ToString();

                        try
                        {
                            taskNotes.NotesId = Convert.ToInt32(reader["notesId"]);
                            taskNotes.WorkHours = Convert.ToDouble(reader["workHours"]);

                            Employee employee = new Employee();
                            employee.EmpID = (int)reader[11];
                            employee.EmpName = reader["empName"].ToString();
                            employee.EmpPassword = reader["empPassword"].ToString();
                            employee.EmpEmail = reader["empEmail"].ToString();
                            taskNotes.Employee = employee;
                        }
                        catch(Exception ex)
                        {

                        }
                            
                            TaskManagementSystem.Models.Task task = new TaskManagementSystem.Models.Task();
                            task.TaskID = (int)reader[0];
                            task.TaskTitle = reader["taskTilte"].ToString();
                            task.DataBaseID = (int)reader[1];
                            task.EmpID = (int)reader["empID"];
                            task.TaskDescription = reader["taskDescription"].ToString();
                            task.IsActive = (bool)reader[8];
                            task.AssignedBy = reader["assignedby"].ToString();
                            task.CategoryID = (int)reader["CategoryID"];

                            taskNotes.task = task;
                            taskNotesList.Add(taskNotes);
                        }
                        return taskNotesList;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        public void InsertTaskNotes(Notes note)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("SPI_TaskNotes", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@taskID", note.TaskId);
                    command.Parameters.AddWithValue("@empID", note.EmpID);
                    command.Parameters.AddWithValue("@notes", note.TaskNotes);
                    command.Parameters.AddWithValue("@workHours", note.WorkHours);
                    command.Parameters.AddWithValue("@isActive",1);
                    connection.Open();
                    command.ExecuteNonQuery();


                }
            }

            catch (Exception ex)
            { 
            }
               
        }
    }
}
