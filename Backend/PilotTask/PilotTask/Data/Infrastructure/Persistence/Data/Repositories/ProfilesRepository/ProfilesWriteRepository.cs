using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository
{
    public class ProfilesWriteRepository : PilotTaskDbContext, IProfilesWriteRepository
    {
        public ProfilesWriteRepository(DbContextOptions options) : base(options)
        {
        }
    }
}
