using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;

namespace PilotTask.Data.Infrastructure.Persistence.EFCore
{
    public class PilotTaskDbContext : DbContext
    {
        public PilotTaskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

    }
}
