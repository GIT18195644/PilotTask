using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.CreateProfiles;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Profiles.DeleteProfiles
{
    public class DeleteProfiles : IConsumer<DeleteProfilesCommand>
    {
        private readonly ILogger<DeleteProfiles> logger;
        private readonly IProfilesService profilesService;

        public DeleteProfiles(ILogger<DeleteProfiles> logger, IProfilesService profilesService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
        }

        public async Task Consume(ConsumeContext<DeleteProfilesCommand> context)
        {
			try
			{
                this.logger.LogInformation("[DeleteProfiles] Event received");

                var profile = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                if (profile != null && profile.ProfileId != null)
                {
                    var result = await this.profilesService.DeleteProfile(context.Message.ProfileId);
                    if (result != null)
                    {
                        var response = new DeleteProfilesResponse
                        {
                            Value = result
                        };

                        await context.RespondAsync(ResponseWrapper<DeleteProfilesResponse>.Success("Profile deleted successfully.", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<DeleteProfilesResponse>.Fail("Failed to delete profile."));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<DeleteProfilesResponse>.Fail("Invalid profile id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[DeleteProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<DeleteProfilesResponse>.Fail(ex.Message));
            }
        }
    }
}
