using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Services.TasksService
{
    public class TasksService : ITasksService
    {
        private readonly ILogger<TasksService> logger;
        private readonly ITasksReadRepository tasksReadRepository;
        private readonly ITasksWriteRepository tasksWriteRepository;

        public TasksService(ILogger<TasksService> logger, ITasksReadRepository tasksReadRepository, ITasksWriteRepository tasksWriteRepository)
        {
            this.logger = logger;
            this.tasksReadRepository = tasksReadRepository;
            this.tasksWriteRepository = tasksWriteRepository;
        }

        public async Task<Tasks?> CreateTask(TaskModel task)
        {
            try
            {
                this.logger.LogInformation($"[TasksService:CreateTask] Event Received");

                return new Tasks();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:CreateTask] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Tasks?> UpdateTask(int taskId, TaskModel task)
        {
            try
            {
                this.logger.LogInformation($"[TasksService:UpdateTask] Event Received");

                return new Tasks();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:UpdateTask] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Tasks?> DeleteTask(int taskId)
        {
            try
            {
                this.logger.LogInformation($"[TasksService:DeleteTask] Event Received");

                return new Tasks();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:DeleteTask] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<List<Tasks>?> RetriveTasks()
        {
            try
            {
                this.logger.LogInformation($"[TasksService:RetriveTasks] Event Received");

                return new List<Tasks>();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:RetriveTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Tasks?> RetriveTasks(int taskId)
        {
            try
            {
                this.logger.LogInformation($"[TasksService:RetriveTasks] Event Received");

                return new Tasks();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:RetriveTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<List<Tasks>?> RetriveTasksByProfileId(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[TasksService:RetriveTasksByProfileId] Event Received");

                return new List<Tasks>();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[TasksService:RetriveTasksByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
