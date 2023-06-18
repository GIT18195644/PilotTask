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

                var res = await this.profilesWriteRepository.CreateProfileDataAsync(profile);
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesService:CreateProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> UpdateProfile(int profileId, ProfileModel profile)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:UpdateProfile] Event Received");

                var res = await this.profilesWriteRepository.UpdateProfileDataAsync(profileId, profile);
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesService:UpdateProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> DeleteProfile(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:DeleteProfile] Event Received");

                var res = await this.profilesWriteRepository.DeleteProfileDataAsync(profileId);
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesService:DeleteProfile] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<List<Profiles>?> RetriveProfiles()
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles] Event Received");

                var res = await this.profilesReadRepository.GetProfileDataAsync();
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> RetriveProfiles(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles:profileId] Event Received");

                var res = await this.profilesReadRepository.GetProfileDataAsync(profileId);
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesService:RetriveProfiles:profileId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
