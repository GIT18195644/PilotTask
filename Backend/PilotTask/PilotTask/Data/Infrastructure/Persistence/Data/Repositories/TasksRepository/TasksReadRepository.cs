using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.TasksRepository
{
    public class TasksReadRepository : PilotTaskDbContext, ITasksReadRepository
    {
        private ILogger<TasksReadRepository> logger;

        public TasksReadRepository(DbContextOptions options, ILogger<TasksReadRepository> logger) : base(options)
        {
            this.logger = logger;
        }

        public async Task<List<Tasks>?> GetTaskDataAsync()
        {
            try
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync] Event Received");

                return new List<Tasks>();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksReadRepository:GetTaskDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Tasks?> GetTaskDataAsync(int taskId)
        {
            try
            {
                this.logger.LogInformation($"[TasksReadRepository:GetTaskDataAsync:taskId] Event Received");

                return new Tasks();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksReadRepository:GetTaskDataAsync:taskId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
