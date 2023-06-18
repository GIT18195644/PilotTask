using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;
using System.Data;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.TasksRepository
{
    public class TasksReadRepository : PilotTaskDbContext, ITasksReadRepository
    {
        private ILogger<TasksReadRepository> logger;
        private readonly PilotTaskDbContext ctx;
        private readonly IConfiguration configuration;

        public TasksReadRepository(DbContextOptions options, ILogger<TasksReadRepository> logger, PilotTaskDbContext ctx, IConfiguration configuration) : base(options)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.configuration = configuration;
        }

        public async Task<List<Tasks>?> GetTaskDataAsync()
        {
            try
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync] Event Received");

                var res = new List<Tasks>();

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "GetTasksDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var task = new Tasks
                                {
                                    Id = reader.GetInt32(0),
                                    ProfileId = reader.GetInt32(1),
                                    TaskName = reader.GetString(2),
                                    TaskDescription = reader.GetString(3),
                                    StartTime = reader.GetDateTime(4),
                                    Status = reader.GetInt32(5)
                                };

                                res.Add(task);
                            }
                        }
                    }
                }

                return (res != null && res.Count > 0) ? res.ToList() : new List<Tasks>();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Tasks?> GetTaskDataAsync(int taskId)
        {
            try
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync:taskId] Event Received");

                var res = new Tasks();

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "GetTasksByIdDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TaskId", taskId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res = new Tasks
                                {
                                    Id = reader.GetInt32(0),
                                    ProfileId = reader.GetInt32(1),
                                    TaskName = reader.GetString(2),
                                    TaskDescription = reader.GetString(3),
                                    StartTime = reader.GetDateTime(4),
                                    Status = reader.GetInt32(5)
                                };
                            }
                        }
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync:taskId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<List<Tasks>?> GetTaskByProfileIdDataAsync(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskByProfileIdDataAsync:taskId] Event Received");

                var res = new List<Tasks>();

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "GetTaskByProfileIdDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProfileId", profileId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var task = new Tasks
                                {
                                    Id = reader.GetInt32(0),
                                    ProfileId = reader.GetInt32(1),
                                    TaskName = reader.GetString(2),
                                    TaskDescription = reader.GetString(3),
                                    StartTime = reader.GetDateTime(4),
                                    Status = reader.GetInt32(5)
                                };

                                res.Add(task);
                            }
                        }
                    }
                }

                return (res != null && res.Count > 0) ? res.ToList() : new List<Tasks>();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskByProfileIdDataAsync:taskId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
