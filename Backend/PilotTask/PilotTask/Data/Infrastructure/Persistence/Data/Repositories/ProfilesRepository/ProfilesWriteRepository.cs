using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;
using PilotTask.Models;
using System.Data;
using System.Data.Entity;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository
{
    public class ProfilesWriteRepository : PilotTaskDbContext, IProfilesWriteRepository
    {
        private ILogger<ProfilesWriteRepository> logger;
        private readonly PilotTaskDbContext ctx;
        private readonly IConfiguration configuration;
        private readonly IProfilesReadRepository profilesReadRepository;

        public ProfilesWriteRepository(DbContextOptions options, ILogger<ProfilesWriteRepository> logger, PilotTaskDbContext ctx, IConfiguration configuration, IProfilesReadRepository profilesReadRepository) : base(options)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.configuration = configuration;
            this.profilesReadRepository = profilesReadRepository;
        }

        public async Task<Profiles?> CreateProfileDataAsync(ProfileModel profile)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:CreateProfileDataAsync] Event Received");

                var res = new Profiles
                {
                    FirstName = (string.IsNullOrEmpty(profile.FirstName)) ? "" : profile.FirstName.Trim(),
                    LastName = (string.IsNullOrEmpty(profile.LastName)) ? "" : profile.LastName.Trim(),
                    DateOfBirth = (profile.DateOfBirth == null) ? null : (DateTime)profile.DateOfBirth,
                    PhoneNumber = (string.IsNullOrEmpty(profile.PhoneNumber)) ? "" : profile.PhoneNumber,
                    EmailId = (string.IsNullOrEmpty(profile.EmailId)) ? "" : profile.EmailId.Trim().ToLower()
                };

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "CreateProfileDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", res.FirstName);
                        command.Parameters.AddWithValue("@LastName", res.LastName);
                        command.Parameters.AddWithValue("@DOB", res.DateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNumber", res.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", res.EmailId);

                        command.ExecuteNonQuery();
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:CreateProfileDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> DeleteProfileDataAsync(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:DeleteProfileDataAsync] Event Received");

                var profile = await this.profilesReadRepository.GetProfileDataAsync(profileId);
                if (profile != null)
                {
                    using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                    {
                        connection.Open();

                        var storedProcedureName = "DeleteProfileDataAsync";

                        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ProfileId", profileId);

                            command.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:DeleteProfileDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<bool?> UpdateProfileDataAsync(int profileId, ProfileModel profile)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:UpdateProfileDataAsync] Event Received");
                
                var res = await this.profilesReadRepository.GetProfileDataAsync(profileId);
                if (res != null)
                {
                    res = new Profiles
                    {
                        ProfileId = profileId,
                        FirstName = (string.IsNullOrEmpty(profile.FirstName)) ? "" : profile.FirstName.Trim(),
                        LastName = (string.IsNullOrEmpty(profile.LastName)) ? "" : profile.LastName.Trim(),
                        DateOfBirth = profile.DateOfBirth,
                        PhoneNumber = (string.IsNullOrEmpty(profile.PhoneNumber)) ? "" : profile.PhoneNumber.Trim(),
                        EmailId = (string.IsNullOrEmpty(profile.EmailId)) ? "" : profile.EmailId.Trim().ToLower()
                    };

                    using (SqlConnection connection = new SqlConnection(configuration["ConnectionStrings:Connection"]))
                    {
                        connection.Open();

                        var storedProcedureName = "UpdateProfileDataAsync";

                        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ProfileId", profileId);
                            command.Parameters.AddWithValue("@FirstName", res.FirstName);
                            command.Parameters.AddWithValue("@LastName", res.LastName);
                            command.Parameters.AddWithValue("@DOB", res.DateOfBirth);
                            command.Parameters.AddWithValue("@PhoneNumber", res.PhoneNumber);
                            command.Parameters.AddWithValue("@Email", res.EmailId);

                            command.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesWriteRepository:UpdateProfileDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
