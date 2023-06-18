using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.EFCore;
using System.Data;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository
{
    public class ProfilesReadRepository : PilotTaskDbContext, IProfilesReadRepository
    {
        private ILogger<ProfilesReadRepository> logger;
        private readonly PilotTaskDbContext ctx;
        private readonly IConfiguration configuration;

        public ProfilesReadRepository(DbContextOptions options, ILogger<ProfilesReadRepository> logger, PilotTaskDbContext ctx, IConfiguration configuration) : base(options)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.configuration = configuration;
        }

        public async Task<List<Profiles>?> GetProfileDataAsync()
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync] Event Received");

                var res = new List<Profiles>();

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "GetProfileDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var profile = new Profiles
                                {
                                    ProfileId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    PhoneNumber = reader.GetString(4),
                                    EmailId = reader.GetString(5)
                                };

                                res.Add(profile);
                            }
                        }
                    }
                }

                return (res != null && res.Count > 0) ? res.ToList() : new List<Profiles>();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> GetProfileDataAsync(int profileId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:ProfileId] Event Received");

                var profile = new Profiles();

                using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                {
                    connection.Open();

                    var storedProcedureName = "GetProfileByIdDataAsync";

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProfileId", profileId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                profile = new Profiles
                                {
                                    ProfileId = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    DateOfBirth = reader.GetDateTime(3),
                                    PhoneNumber = reader.GetString(4),
                                    EmailId = reader.GetString(5)
                                };
                            }
                        }
                    }
                }

                return profile;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:ProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }

        public async Task<Profiles?> GetProfileDataAsync(string emailId)
        {
            try
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:EmailId] Event Received");

                if (!string.IsNullOrEmpty(emailId))
                {
                    var profile = new Profiles();

                    using (SqlConnection connection = new SqlConnection(this.configuration["ConnectionStrings:Connection"]))
                    {
                        connection.Open();

                        var storedProcedureName = "GetProfileByEmailDataAsync";

                        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Email", emailId);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    profile = new Profiles
                                    {
                                        ProfileId = reader.GetInt32(0),
                                        FirstName = reader.GetString(1),
                                        LastName = reader.GetString(2),
                                        DateOfBirth = reader.GetDateTime(3),
                                        PhoneNumber = reader.GetString(4),
                                        EmailId = reader.GetString(5)
                                    };
                                }
                            }
                        }
                    }

                    return profile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesReadRepository:GetProfileDataAsync:EmailId] Exception occurred: Inner exception: {ex.InnerException}");
                return null;
            }
        }
    }
}
