using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.CreateProfiles;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Models;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Profiles.UpdateProfiles
{
    public class UpdateProfiles : IConsumer<UpdateProfilesCommand>
    {
        private readonly ILogger<UpdateProfiles> logger;
        private readonly IProfilesService profilesService;

        public UpdateProfiles(ILogger<UpdateProfiles> logger, IProfilesService profilesService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
        }

        public async Task Consume(ConsumeContext<UpdateProfilesCommand> context)
        {
            try
            {
                this.logger.LogInformation("[UpdateProfiles] Event received");

                var profile = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                if (profile != null && profile.ProfileId != null)
                {
                    if (string.IsNullOrEmpty(context.Message.FirstName))
                    {
                        await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Fail("First name is required."));
                    }

                    if (string.IsNullOrEmpty(context.Message.EmailId))
                    {
                        await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Fail("Email is required."));
                    }

                    var data = new ProfileModel
                    {
                        FirstName = context.Message.FirstName,
                        LastName = context.Message.LastName,
                        EmailId = context.Message.EmailId,
                        DateOfBirth = context.Message.DateOfBirth,
                        PhoneNumber = context.Message.PhoneNumber
                    };

                    var result = await this.profilesService.UpdateProfile(context.Message.ProfileId, data);
                    if (result != null)
                    {
                        var response = new UpdateProfilesResponse
                        {
                            Value = result
                        };

                        await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Success("Profile updated successfully.", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Fail("Failed to update profile."));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Fail("Invalid profile id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[UpdateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<UpdateProfilesResponse>.Fail(ex.Message));
            }
        }
    }
}
