using MassTransit;
using PilotTask.Controllers;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Queries.Tasks.GetTasksByProfileId
{
    public class GetTasksByProfileId : IConsumer<GetTasksByProfileIdQuery>
    {
        private readonly ILogger<GetTasksByProfileId> logger;
        private readonly IProfilesService profilesService;
        private readonly ITasksService tasksService;

        public GetTasksByProfileId(ILogger<GetTasksByProfileId> logger, IProfilesService profilesService, ITasksService tasksService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
            this.tasksService = tasksService;
        }

        public async Task Consume(ConsumeContext<GetTasksByProfileIdQuery> context)
        {
			try
			{
                this.logger.LogInformation("[GetTasksByProfileId] Event received");

                var profile = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                if (profile != null && profile.ProfileId != null)
                {
                    var result = await this.tasksService.RetriveTasksByProfileId(context.Message.ProfileId);
                    if (result != null)
                    {
                        var response = new GetTasksByProfileIdResponse
                        {
                            Values = new GetTasksViewModel
                            {
                                Tasks = (result.Count > 0) ? result.ToList() : new List<Entities.Tasks>()
                            }
                        };

                        await context.RespondAsync(ResponseWrapper<GetTasksByProfileIdResponse>.Success("Tasks get successfully", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<GetTasksByProfileIdResponse>.Fail("Failed to get tasks details."));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<GetTasksByProfileIdResponse>.Fail("Invalid profile id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[GetTasksByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<GetTasksByProfileIdResponse>.Fail(ex.Message));
            }
        }
    }
}
