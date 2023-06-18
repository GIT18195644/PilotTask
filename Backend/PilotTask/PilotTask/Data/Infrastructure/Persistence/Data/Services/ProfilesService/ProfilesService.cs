using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Services.ProfilesService
{
    public class ProfilesService : IProfilesService
    {
        private readonly ILogger<ProfilesService> logger;
        private readonly IProfilesReadRepository profilesReadRepository;
        private readonly IProfilesWriteRepository profilesWriteRepository;

        public ProfilesService(ILogger<ProfilesService> logger, IProfilesReadRepository profilesReadRepository, IProfilesWriteRepository profilesWriteRepository)
        {
            this.logger = logger;
            this.profilesReadRepository = profilesReadRepository;
            this.profilesWriteRepository = profilesWriteRepository;
        }

        public async Task<Profiles?> CreateProfile(ProfileModel profile)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:CreateProfile] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesService:CreateProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> UpdateProfile(int profileId, ProfileModel profile)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:UpdateProfile] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesService:UpdateProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> DeleteProfile(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:DeleteProfile] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesService:DeleteProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<List<Profiles>?> RetriveProfiles()
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles] Event Received");

                return new List<Profiles>();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesService:RetriveProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> RetriveProfiles(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles:profileId] Event Received");

                return new Profiles();
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesService:RetriveProfiles:profileId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
