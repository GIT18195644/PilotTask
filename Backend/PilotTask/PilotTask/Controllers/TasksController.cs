using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Models;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> logger;
        private readonly ITasksService tasksService;

        public TasksController(ILogger<TasksController> logger, ITasksService tasksService)
        {
            this.logger = logger;
            this.tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await this.tasksService.RetriveTasks();
                if (result != null)
                {
                    var response = new GetTasksViewModel
                    {
                        Tasks = (result.Count > 0) ? result.ToList() : new List<Tasks>()
                    };
                    return Ok(ResponseWrapper<GetTasksViewModel>.Success("Tasks get successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<GetTasksViewModel>.Fail("Failed to get tasks details"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:GetTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetTasksViewModel>.Fail(ex.Message));
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTasksByTaskId(int taskId)
        {
            try
            {
                var result = await this.tasksService.RetriveTasks(taskId);
                if (result != null)
                {
                    var response = new GetTasksByTaskIdViewModel
                    {
                        Task = result
                    };
                    return Ok(ResponseWrapper<GetTasksByTaskIdViewModel>.Success("Task get successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<GetTasksByTaskIdViewModel>.Fail("Failed to get task details"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:GetTasksByTaskId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetTasksByTaskIdViewModel>.Fail(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTasks(TaskModel task)
        {
            try
            {
                var result = await this.tasksService.CreateTask(task);
                if (result != null)
                {
                    var response = new CreateTaskViewModel
                    {
                        Task = result
                    };
                    return Created("", ResponseWrapper<CreateTaskViewModel>.Success("Task created successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<CreateTaskViewModel>.Fail("Failed to create task"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:CreateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<CreateTaskViewModel>.Fail(ex.Message));
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTasks(int taskId, TaskModel task)
        {
            try
            {
                //var result = await this.tasksService.UpdateTask(taskId, task);
                //if (result != null)
                //{
                //    var response = new UpdateTaskViewModel
                //    {
                //        Task = result
                //    };
                //    return Ok(ResponseWrapper<UpdateTaskViewModel>.Success("Task updated successfully", response));
                //}
                //else
                //{
                //    return BadRequest(ResponseWrapper<UpdateTaskViewModel>.Fail("Failed to update task"));
                //}

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:UpdateTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<UpdateTaskViewModel>.Fail(ex.Message));

            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTasks(int taskId)
        {
            try
            {
                //var result = await this.tasksService.DeleteTask(taskId);
                //if (result != null)
                //{
                //    var response = new DeleteTaskViewModel
                //    {
                //        Task = result
                //    };
                //    return Ok(ResponseWrapper<DeleteTaskViewModel>.Success("Task deleted successfully", response));
                //}
                //else
                //{
                //    return BadRequest(ResponseWrapper<DeleteTaskViewModel>.Fail("Failed to delete task"));
                //}

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[TasksController:DeleteTasks] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<DeleteTaskViewModel>.Fail(ex.Message));
            }
        }
    }
}
