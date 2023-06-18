using Azure;
using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Application.Queries.Profiles.GetProfiles;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Queries.Profiles.GetProfilesByProfileId
{
    public class GetProfilesByProfileId : IConsumer<GetProfilesByProfileIdQuery>
    {
        private readonly ILogger<GetProfilesByProfileId> logger;
        private readonly IProfilesService profilesService;

        public GetProfilesByProfileId(ILogger<GetProfilesByProfileId> logger, IProfilesService profilesService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
        }

        public async Task Consume(ConsumeContext<GetProfilesByProfileIdQuery> context)
        {
			try
			{
                this.logger.LogInformation("[GetProfilesByProfileId] Event received");

                var profile = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                if (profile != null && profile.ProfileId != null)
                {
                    var result = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                    if (result != null)
                    {
                        var response = new GetProfilesByProfileIdResponse
                        {
                            Value = new GetProfilesByProfileIdViewModel
                            {
                                Profile = profile
                            }
                        };

                        await context.RespondAsync(ResponseWrapper<GetProfilesByProfileIdResponse>.Success("Profile get successfully", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<GetProfilesByProfileIdResponse>.Fail("Failed to get profile details"));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<GetProfilesByProfileIdResponse>.Fail("Invalid profile id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[GetProfilesByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<GetProfilesByProfileIdResponse>.Fail(ex.Message));
            }
        }
    }
}
