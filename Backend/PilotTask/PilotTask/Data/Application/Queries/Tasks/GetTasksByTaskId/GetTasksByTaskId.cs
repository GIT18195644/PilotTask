using MassTransit;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByProfileId;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Data.Application.Queries.Tasks.GetTasksByTaskId
{
    public class GetTasksByTaskId : IConsumer<GetTasksByTaskIdQuery>
    {
        private readonly ILogger<GetTasksByTaskIdQuery> logger;
        private readonly ITasksService tasksService;

        public GetTasksByTaskId(ILogger<GetTasksByTaskIdQuery> logger, ITasksService tasksService)
        {
            this.logger = logger;
            this.tasksService = tasksService;
        }

        public async Task Consume(ConsumeContext<GetTasksByTaskIdQuery> context)
        {
			try
			{
                this.logger.LogInformation("[GetTasksByTaskId] Event received");

                var result = await this.tasksService.RetriveTasks(context.Message.TaskId);
                if (result != null && result.Id != null)
                {
                    var response = new GetTasksByTaskIdResponse
                    {
                        Value = new Views.GetTasksByTaskIdViewModel
                        {
                            Task = result
                        }
                    };

                    await context.RespondAsync(ResponseWrapper<GetTasksByTaskIdResponse>.Success("Task get successfully", response));
                }
                else
                {
                    await context.RespondAsync(ResponseWrapper<GetTasksByTaskIdResponse>.Fail("Invalid task id."));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[GetTasksByTaskId] Exception occurred: Inner exception: {ex.InnerException}");
                await context.RespondAsync(ResponseWrapper<GetTasksByTaskIdResponse>.Fail(ex.Message));
            }
        }
    }
}
