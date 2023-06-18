using PilotTask.Data.Entities;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository
{
    public interface ITasksWriteRepository
    {
        public Task<Tasks?> CreateTaskDataAsync(TaskModel task);
        public Task<bool?> UpdateTaskDataAsync(int taskId, TaskModel task);
        public Task<bool?> DeleteTaskDataAsync(int taskId);
    }
}
