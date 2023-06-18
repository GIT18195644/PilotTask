using Azure;
using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Queries.Profiles.GetProfiles
{
    public class GetProfiles : IConsumer<GetProfilesQuery>
    {
        private readonly ILogger<GetProfiles> logger;
        private readonly IProfilesService profilesService;

        public GetProfiles(ILogger<GetProfiles> logger, IProfilesService profilesService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
        }

        public async Task Consume(ConsumeContext<GetProfilesQuery> context)
        {
			try
			{
                this.logger.LogInformation("[GetProfiles] Event received");

                var result = await this.profilesService.RetriveProfiles();
                if (result != null && result.Count > 0)
                {
                    var response = new GetProfilesResponse
                    {
                        Values = new GetProfilesViewModel
                        {
                            Profiles = (result.Count > 0) ? result.ToList() : new List<Entities.Profiles>()
                        }
                    };

                    await context.RespondAsync(ResponseWrapper<GetProfilesResponse>.Success("Profiles get successfully.", response));
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<GetProfilesResponse>.Fail("Failed to get profiles details."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[GetProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<GetProfilesResponse>.Fail(ex.Message));
            }
        }
    }
}
