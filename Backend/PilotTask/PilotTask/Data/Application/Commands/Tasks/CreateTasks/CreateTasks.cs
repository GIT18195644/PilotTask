using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByProfileId;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Tasks.CreateTasks
{
    public class CreateTasks : IConsumer<CreateTasksCommand>
    {
        private readonly ILogger<CreateTasks> logger;
        private readonly IProfilesService profilesService;
        private readonly ITasksService tasksService;

        public CreateTasks(ILogger<CreateTasks> logger, IProfilesService profilesService, ITasksService tasksService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
            this.tasksService = tasksService;
        }

        public async Task Consume(ConsumeContext<CreateTasksCommand> context)
        {
			try
			{
                this.logger.LogInformation("[CreateTasks] Event received");

                var profile = await this.profilesService.RetriveProfiles(context.Message.ProfileId);
                if (profile != null && profile.ProfileId != null)
                {
                    var task = new TaskModel
                    {
                        ProfileId = context.Message.ProfileId,
                        TaskName = context.Message.TaskName,
                        TaskDescription = context.Message.TaskDescription,
                        StartTime = context.Message.StartTime,
                        Status = context.Message.Status
                    };

                    var result = await this.tasksService.CreateTask(task);
                    if (result != null)
                    {
                        var response = new CreateTasksResponse
                        {
                            Value = new Views.CreateTaskViewModel
                            {
                                Task = result
                            }
                        };

                        await context.RespondAsync(ResponseWrapper<CreateTasksResponse>.Success("Task created successfully", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<CreateTasksResponse>.Fail("Failed to create task"));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<CreateTasksResponse>.Fail("Invalid profile id."));
                }

            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[CreateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<CreateTasksResponse>.Fail(ex.Message));
            }
        }
    }
}
