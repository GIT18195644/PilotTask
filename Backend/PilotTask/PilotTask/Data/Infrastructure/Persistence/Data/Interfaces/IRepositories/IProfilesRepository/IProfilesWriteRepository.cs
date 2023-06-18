﻿using PilotTask.Data.Entities;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository
{
    public interface IProfilesWriteRepository
    {
        public Task<Profiles?> CreateProfileDataAsync(ProfileModel profile);
        public Task<Profiles?> UpdateProfileDataAsync(int profileId, ProfileModel profile);
        public Task<Profiles?> DeleteProfileDataAsync(int profileId);
    }
}
