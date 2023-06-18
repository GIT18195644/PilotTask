using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;
using PilotTask.Models;
using System.Data;
using System.Threading.Tasks;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.TasksRepository
{
    public class TasksWriteRepository : PilotTaskDbContext, ITasksWriteRepository
    {
        private ILogger<ProfilesWriteRepository> logger;
        private readonly PilotTaskDbContext ctx;
        private readonly IConfiguration configuration;
        private readonly ITasksReadRepository tasksReadRepository;

        public TasksWriteRepository(DbContextOptions options, ILogger<ProfilesWriteRepository> logger, PilotTaskDbContext ctx, IConfiguration configuration, ITasksReadRepository tasksReadRepository) : base(options)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.configuration = configuration;
            this.tasksReadRepository = tasksReadRepository;
        }

        public async Task<Tasks?> CreateTaskDataAsync(TaskModel task)
        {
            try
            {
                this.logger.LogInformation($"[TasksWriteRepository:CreateTaskDataAsync] Event Received");

                var res = new Tasks
                {
                    ProfileId = task.ProfileId,
                    TaskName = (string.IsNullOrEmpty(task.TaskName)) ? "" : task.TaskName.Trim(),
                    TaskDescription = (string.IsNullOrEmpty(task.TaskDescription)) ? "" : task.TaskDescription.Trim(),
                    StartTime = task.StartTime,
                    Status = task.Status
                };

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "CreateTaskDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProfileId", res.ProfileId);
                        command.Parameters.AddWithValue("@TaskName", res.TaskName);
                        command.Parameters.AddWithValue("@TaskDescription", res.TaskDescription);
                        command.Parameters.AddWithValue("@StartTime", res.StartTime);
                        command.Parameters.AddWithValue("@Status", res.Status);

                        command.ExecuteNonQuery();
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksWriteRepository:CreateTaskDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> DeleteTaskDataAsync(int taskId)
        {
            try
            {
                this.logger.LogInformation($"[TasksWriteRepository:DeleteTaskDataAsync] Event Received");

                var res = await this.tasksReadRepository.GetTaskDataAsync(taskId);
                if (res != null)
                {
                    using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                    {
                        connection.Open();

                        var storedProcedureName = "DeleteTaskDataAsync";

                        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TaskId", taskId);

                            command.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksWriteRepository:DeleteTaskDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> UpdateTaskDataAsync(int taskId, TaskModel task)
        {
            try
            {
                this.logger.LogInformation($"[TasksWriteRepository:UpdateTaskDataAsync] Event Received");

                var res = await this.tasksReadRepository.GetTaskDataAsync(taskId);
                if (res != null)
                {
                    res = new Tasks
                    {
                        Id = taskId,
                        ProfileId = task.ProfileId,
                        TaskName = (string.IsNullOrEmpty(task.TaskName)) ? "" : task.TaskName.Trim(),
                        TaskDescription = (string.IsNullOrEmpty(task.TaskDescription)) ? "" : task.TaskDescription.Trim(),
                        StartTime = task.StartTime,
                        Status = task.Status
                    };

                    using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                    {
                        connection.Open();

                        var storedProcedureName = "UpdateTaskDataAsync";

                        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TaskId", res.Id);
                            command.Parameters.AddWithValue("@ProfileId", res.ProfileId);
                            command.Parameters.AddWithValue("@TaskName", res.TaskName);
                            command.Parameters.AddWithValue("@TaskDescription", res.TaskDescription);
                            command.Parameters.AddWithValue("@StartTime", res.StartTime);
                            command.Parameters.AddWithValue("@Status", res.Status);

                            command.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksWriteRepository:UpdateTaskDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
