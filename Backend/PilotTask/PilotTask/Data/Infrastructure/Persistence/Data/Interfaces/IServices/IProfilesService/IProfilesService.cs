using PilotTask.Data.Entities;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService
{
    public interface IProfilesService
    {
        public Task<Profiles?> CreateProfile(ProfileModel profile);

        public Task<bool?> UpdateProfile(int profileId, ProfileModel profile);

        public Task<bool?> DeleteProfile(int profileId);

        public Task<List<Profiles>?> RetriveProfiles();

        public Task<Profiles?> RetriveProfiles(int profileId);
    }
}
