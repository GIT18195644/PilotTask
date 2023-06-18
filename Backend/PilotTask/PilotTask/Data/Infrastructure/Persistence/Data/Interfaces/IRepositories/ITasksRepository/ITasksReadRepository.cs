using PilotTask.Data.Entities;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository
{
    public interface ITasksReadRepository
    {
        public Task<List<Tasks>?> GetTaskDataAsync();
        public Task<Tasks?> GetTaskDataAsync(int taskId);
    }
}
