using MassTransit;
using PilotTask.Data.Application.Commands.Tasks.CreateTasks;
using PilotTask.Data.Application.Commands.Tasks.UpdateTasks;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Commands.Tasks.DeleteTasks
{
    public class DeleteTasks : IConsumer<DeleteTasksCommand>
    {
        private readonly ILogger<DeleteTasks> logger;
        private readonly ITasksService tasksService;

        public DeleteTasks(ILogger<DeleteTasks> logger, ITasksService tasksService)
        {
            this.logger = logger;
            this.tasksService = tasksService;
        }

        public async Task Consume(ConsumeContext<DeleteTasksCommand> context)
        {
			try
			{
                this.logger.LogInformation("[DeleteTasks] Event received");

                var result = await this.tasksService.RetriveTasks(context.Message.TaskId);
                if (result != null && result.Id != null)
                {
                    var data = await this.tasksService.DeleteTask(context.Message.TaskId);
                    if (result != null)
                    {
                        var response = new DeleteTasksResponse
                        {
                            Value = data
                        };

                        await context.RespondAsync(ResponseWrapper<DeleteTasksResponse>.Success("Task deleted successfully", response));
                    }
                    else
                    {
                        await context.RespondAsync(ResponseWrapper<DeleteTasksResponse>.Fail("Failed to delete task"));
                    }
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<DeleteTasksResponse>.Fail("Invalid task id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[DeleteTasks] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<DeleteTasksResponse>.Fail(ex.Message));
            }
        }
    }
}
