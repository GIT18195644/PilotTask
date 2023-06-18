using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotTask.Data.Application.Commands.Tasks.CreateTasks;
using PilotTask.Data.Application.Commands.Tasks.DeleteTasks;
using PilotTask.Data.Application.Commands.Tasks.UpdateTasks;
using PilotTask.Data.Application.Queries.Profiles.GetProfiles;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByTaskId;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;
using System.Threading.Tasks;

namespace PilotTask.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;
        private readonly IMediator mediator;

        public TasksController(ILogger<TasksController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTasksByTaskId(int taskId)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetTasksByTaskIdQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetTasksByTaskIdResponse>>(new GetTasksByTaskIdQuery {
                    TaskId = taskId
                });

                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:GetTasksByTaskId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetTasksByTaskIdResponse>.Fail(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTasks(TaskModel task)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<CreateTasksCommand>();
                var response = await client.GetResponse<ResponseWrapper<CreateTasksResponse>>(new CreateTasksCommand { 
                    ProfileId = task.ProfileId,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    StartTime = task.StartTime,
                    Status = task.Status
                });

                if (response.Message.Succeeded)
                {
                    return Created(response.Message.Message, response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:CreateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<CreateTasksResponse>.Fail(ex.Message));
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTasks(int taskId, TaskModel task)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<UpdateTasksCommand>();
                var response = await client.GetResponse<ResponseWrapper<UpdateTasksResponse>>(new UpdateTasksCommand
                {
                    TaskId = taskId,
                    ProfileId = task.ProfileId,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    StartTime = task.StartTime,
                    Status = task.Status
                });

                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:UpdateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<UpdateTasksResponse>.Fail(ex.Message));

            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTasks(int taskId)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<DeleteTasksCommand>();
                var response = await client.GetResponse<ResponseWrapper<DeleteTasksResponse>>(new DeleteTasksCommand
                {
                    TaskId = taskId
                });

                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:DeleteTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<DeleteTasksResponse>.Fail(ex.Message));
            }
        }
    }
}
