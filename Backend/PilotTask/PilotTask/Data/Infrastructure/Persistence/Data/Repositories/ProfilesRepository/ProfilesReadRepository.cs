using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository
{
    public class ProfilesReadRepository : PilotTaskDbContext, IProfilesReadRepository
    {
        private ILogger<ProfilesReadRepository> logger;

        public ProfilesReadRepository(DbContextOptions options, ILogger<ProfilesReadRepository> logger) : base(options)
        {
            this.logger = logger;
        }

        public async Task<List<Profiles>?> GetProfileDataAsync()
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync] Event Received");

                return new List<Profiles>();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesReadRepository:GetProfileDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> GetProfileDataAsync(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:ProfileId] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesReadRepository:GetProfileDataAsync:ProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> GetProfileDataAsync(string emailId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:EmailId] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesReadRepository:GetProfileDataAsync:EmailId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
