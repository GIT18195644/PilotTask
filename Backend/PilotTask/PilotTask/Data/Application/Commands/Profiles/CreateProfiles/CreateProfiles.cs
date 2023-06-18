using MassTransit;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Models;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Profiles.CreateProfiles
{
    public class CreateProfiles : IConsumer<CreateProfilesCommand>
    {
        private readonly ILogger<CreateProfiles> logger;
        private readonly IProfilesService profilesService;

        public CreateProfiles(ILogger<CreateProfiles> logger, IProfilesService profilesService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
        }

        public async Task Consume(ConsumeContext<CreateProfilesCommand> context)
        {
			try
			{
                this.logger.LogInformation("[CreateProfiles] Event received");

                if (string.IsNullOrEmpty(context.Message.FirstName))
                {
                    await context.RespondAsync(ResponseWrapper<CreateProfilesResponse>.Fail("First name is required."));
                }

                if (string.IsNullOrEmpty(context.Message.EmailId))
                {
                    await context.RespondAsync(ResponseWrapper<CreateProfilesResponse>.Fail("Email is required."));
                }

                var profile = new ProfileModel
                {
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName,
                    EmailId = context.Message.EmailId,
                    DateOfBirth = context.Message.DateOfBirth,
                    PhoneNumber = context.Message.PhoneNumber
                };

                var result = await this.profilesService.CreateProfile(profile);
                if (result != null)
                {
                    var response = new CreateProfilesResponse
                    {
                        Value = new Views.CreateProfileViewModel
                        {
                            Profile = result
                        }
                    };

                    await context.RespondAsync(ResponseWrapper<CreateProfilesResponse>.Success("Profile created successfully.", response));
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<CreateProfilesResponse>.Fail("Failed to create profile."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[CreateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<CreateProfilesResponse>.Fail(ex.Message));
            }
        }
    }
}
