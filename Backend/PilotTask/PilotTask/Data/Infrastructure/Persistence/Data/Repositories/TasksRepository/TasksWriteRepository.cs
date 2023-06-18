using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.TasksRepository
{
    public class TasksWriteRepository : PilotTaskDbContext, ITasksWriteRepository
    {
        public TasksWriteRepository(DbContextOptions options) : base(options)
        {
        }
    }
}
