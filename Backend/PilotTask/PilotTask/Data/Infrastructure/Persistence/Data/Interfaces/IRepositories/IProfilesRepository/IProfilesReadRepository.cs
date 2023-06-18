using PilotTask.Data.Entities;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository
{
    public interface IProfilesReadRepository
    {
        public Task<List<Profiles>?> GetProfileDataAsync();
        public Task<Profiles?> GetProfileDataAsync(int profileId);
        public Task<Profiles?> GetProfileDataAsync(string emailId);
    }
}
