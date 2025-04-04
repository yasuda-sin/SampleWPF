using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal class TaskService : ITaskService
    {
        private readonly string _connectionString = "";
        public async Task<List<TaskItem>> GetAllTaskItemsAsync()
        {
            var taskItem = new List<TaskItem>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    var cmd = new SqlCommand("select * from tasks;", con);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            taskItem.Add(new TaskItem
                            {
                                TaskId = (int)reader["TASK_ID"],
                                TaskName = (string)reader["TASK_NAME"],
                                Description = (string)reader["DESCRIPTION"],
                                Status = (Byte)reader["STATUS"],
                                CreatedDateTime = (DateTime)reader["CREATED_DATETIME"],
                                UpdatedDateTime = reader["UPDATED_DATETIME"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UPDATED_DATETIME"],
                                Deadline = (DateTime)reader["DEADLINE"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return taskItem;
        }

        public async Task<TaskItem?> GetTaskItemByIdAsync(int taskId)
        {
            var taskitem = new TaskItem { TaskName = "", Description = "" };
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    var cmd = new SqlCommand("select * from tasks where taskId = @taskId;", con);
                    cmd.Parameters.AddWithValue("id", taskId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                taskitem =  new TaskItem
                                {
                                    TaskId = (int)reader["TaskId"],
                                    TaskName = (string)reader["TaskName"],
                                    Description = (string)reader["Description"],
                                    Status = (int)reader["Status"],
                                    CreatedDateTime = (DateTime)reader["CreatedDateTime"],
                                    UpdatedDateTime = reader["UpdatedDateTime"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdatedDateTime"],
                                    Deadline = (DateTime)reader["Deadline"]
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
            return null;
        }

        public async Task<bool> AddTaskItemAsync(TaskItem taskItem)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    var cmd = new SqlCommand(@"
                        INSERT INTO Tasks (TaskName, Description, Status, CreatedDateTime, Deadline) 
                        VALUES (@TaskName, @Description, @Status, @CreatedDateTime, @Deadline)", con);
                    cmd.Parameters.AddWithValue("@TaskName", taskItem.TaskName);
                    cmd.Parameters.AddWithValue("@Description", taskItem.Description);
                    cmd.Parameters.AddWithValue("@Status", taskItem.Status);
                    cmd.Parameters.AddWithValue("@CreatedDateTime", taskItem.CreatedDateTime);
                    cmd.Parameters.AddWithValue("@Deadline", taskItem.Deadline);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateTaskItemAsync(TaskItem taskItem)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    var cmd = new SqlCommand(@"
                        UPDATE Tasks 
                        SET TaskName = @TaskName, Description = @Description, Status = @Status, UpdatedDateTime = @UpdatedDateTime, Deadline = @Deadline 
                        WHERE TaskId = @TaskId", con);
                    cmd.Parameters.AddWithValue("@TaskId", taskItem.TaskId);
                    cmd.Parameters.AddWithValue("@TaskName", taskItem.TaskName);
                    cmd.Parameters.AddWithValue("@Description", taskItem.Description);
                    cmd.Parameters.AddWithValue("@Status", taskItem.Status);
                    cmd.Parameters.AddWithValue("@UpdatedDateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Deadline", taskItem.Deadline);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteTaskItemAsync(int taskId)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    var cmd = new SqlCommand("DELETE FROM Tasks WHERE TaskId = @TaskId", con);
                    cmd.Parameters.AddWithValue("@TaskId", taskId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
            return true;
        }
    }
}
