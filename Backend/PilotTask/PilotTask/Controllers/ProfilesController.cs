using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotTask.Data.Application.Commands.Profiles.CreateProfiles;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Application.Commands.Profiles.UpdateProfiles;
using PilotTask.Data.Application.Queries.Profiles.GetProfiles;
using PilotTask.Data.Application.Queries.Profiles.GetProfilesByProfileId;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByProfileId;
using PilotTask.Data.Entities;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Data.Infrastructure.Persistence.Data.Services.ProfilesService;
using PilotTask.Models;
using PilotTask.Views;
using PilotTask.Wrappers.ResponseWrapper;

namespace PilotTask.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> logger;
        private readonly IMediator mediator;

        public ProfilesController(ILogger<ProfilesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetProfilesQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetProfilesResponse>>(new GetProfilesQuery {});

                if (response.Message.Succeeded)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesController:GetProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetProfilesResponse>.Fail(ex.Message));
            }
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfilesByProfileId(int profileId)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetProfilesByProfileIdQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetProfilesByProfileIdResponse>>(new GetProfilesByProfileIdQuery {
                    ProfileId = profileId
                });

                if (response.Message.Succeeded)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesController:GetProfilesByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetProfilesByProfileIdResponse>.Fail(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfiles(ProfileModel profile)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<CreateProfilesCommand>();
                var response = await client.GetResponse<ResponseWrapper<CreateProfilesResponse>>(new CreateProfilesCommand
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    EmailId = profile.EmailId,
                    PhoneNumber = profile.PhoneNumber,
                    DateOfBirth = profile.DateOfBirth
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
                this.logger.LogInformation($"[ProfilesController:CreateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<CreateProfilesResponse>.Fail(ex.Message));
            }
        }

        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfiles(int profileId, ProfileModel profile)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<UpdateProfilesCommand>();
                var response = await client.GetResponse<ResponseWrapper<UpdateProfilesResponse>>(new UpdateProfilesCommand
                {
                    ProfileId = profileId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    EmailId = profile.EmailId,
                    PhoneNumber = profile.PhoneNumber,
                    DateOfBirth = profile.DateOfBirth
                });

                if (response.Message.Succeeded)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesController:UpdateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<UpdateProfilesResponse>.Fail(ex.Message));
            }
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfiles(int profileId)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<DeleteProfilesCommand>();
                var response = await client.GetResponse<ResponseWrapper<DeleteProfilesResponse>>(new DeleteProfilesCommand
                {
                    ProfileId = profileId
                });

                if (response.Message.Succeeded)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesController:DeleteProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<DeleteProfilesResponse>.Fail(ex.Message));
            }
        }

        [HttpGet("{profileId}/tasks")]
        public async Task<IActionResult> GetTasksByProfileId(int profileId)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetTasksByProfileIdQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetTasksByProfileIdResponse>>(new GetTasksByProfileIdQuery
                {
                    ProfileId = profileId
                });

                if (response.Message.Succeeded)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"[ProfilesController:GetTasksByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetTasksByProfileIdResponse>.Fail(ex.Message));
            }
        }
    }
}
