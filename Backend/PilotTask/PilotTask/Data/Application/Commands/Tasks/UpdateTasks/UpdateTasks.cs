using MassTransit;
using PilotTask.Data.Application.Commands.Tasks.CreateTasks;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByTaskId;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Tasks.UpdateTasks
{
    public class UpdateTasks : IConsumer<UpdateTasksCommand>
    {
        private readonly ILogger<UpdateTasks> logger;
        private readonly IProfilesService profilesService;
        private readonly ITasksService tasksService;

        public UpdateTasks(ILogger<UpdateTasks> logger, IProfilesService profilesService, ITasksService tasksService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
            this.tasksService = tasksService;
        }

        public async Task Consume(ConsumeContext<UpdateTasksCommand> context)
        {
            try
            {
                this.logger.LogInformation("[UpdateTasks] Event received");

                var result = await this.tasksService.RetriveTasks(context.Message.TaskId);
                if (result != null && result.Id != null)
                {
                    var task = new TaskModel
                    {
                        ProfileId = context.Message.ProfileId,
                        TaskName = context.Message.TaskName,
                        TaskDescription = context.Message.TaskDescription,
                        StartTime = context.Message.StartTime,
                        Status = context.Message.Status
                    };

                    var data = await this.tasksService.UpdateTask(context.Message.TaskId, task);
                    if (data != null)
                    {
                        var response = new UpdateTasksResponse
                        {
                            Value = data
                        };

                        await context.RespondAsync(ResponseWrapper<UpdateTasksResponse>.Success("Task updated successfully.", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<UpdateTasksResponse>.Fail("Failed to update task."));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<UpdateTasksResponse>.Fail("Invalid task id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[UpdateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<UpdateTasksResponse>.Fail(ex.Message));
            }
        }
    }
}
