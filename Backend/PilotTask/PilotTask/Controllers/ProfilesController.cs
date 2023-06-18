using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IProfilesService profilesService;
        private readonly ITasksService tasksService;

        public ProfilesController(ILogger<ProfilesController> logger, IProfilesService profilesService, ITasksService tasksService)
        {
            this.logger = logger;
            this.profilesService = profilesService;
            this.tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                var result = await this.profilesService.RetriveProfiles();
                if (result != null)
                {
                    var response = new GetProfilesViewModel
                    {
                        Profiles = (result.Count > 0) ? result.ToList() : new List<Profiles>()
                    };
                    return Ok(ResponseWrapper<GetProfilesViewModel>.Success("Profiles get successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<GetProfilesViewModel>.Fail("Failed to get profiles details"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesController:GetProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetProfilesViewModel>.Fail(ex.Message));
            }
        }

        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetProfilesByProfileId(int profileId)
        {
            try
            {
                var result = await this.profilesService.RetriveProfiles(profileId);
                if (result != null)
                {
                    var response = new GetProfilesByProfileIdViewModel
                    {
                        Profile = result
                    };
                    return Ok(ResponseWrapper<GetProfilesByProfileIdViewModel>.Success("Profile get successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<GetProfilesByProfileIdViewModel>.Fail("Failed to get profile details"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesController:GetProfilesByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetProfilesByProfileIdViewModel>.Fail(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfiles(ProfileModel profile)
        {
            try
            {
                var result = await this.profilesService.CreateProfile(profile);
                if (result != null)
                {
                    var response = new CreateProfileViewModel
                    {
                        Profile = result
                    };
                    return Created("", ResponseWrapper<CreateProfileViewModel>.Success("Profile created successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<CreateProfileViewModel>.Fail("Failed to create profile"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesController:CreateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<CreateProfileViewModel>.Fail(ex.Message));
            }
        }

        [HttpPut("{profileId}")]
        public async Task<IActionResult> UpdateProfiles(int profileId, ProfileModel profile)
        {
            try
            {
                var result = await this.profilesService.UpdateProfile(profileId, profile);
                if (result != null)
                {
                    var response = new UpdateProfileViewModel
                    {
                        Profile = result
                    };
                    return Ok(ResponseWrapper<UpdateProfileViewModel>.Success("Profile updated successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<UpdateProfileViewModel>.Fail("Failed to update profile"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesController:UpdateProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfiles(int profileId)
        {
            try
            {
                var result = await this.profilesService.DeleteProfile(profileId);
                if (result != null)
                {
                    var response = new DeleteProfileViewModel
                    {
                        Profile = result
                    };
                    return Ok(ResponseWrapper<DeleteProfileViewModel>.Success("Profile deleted successfully", response));
                }
                else
                {
                    return BadRequest(ResponseWrapper<DeleteProfileViewModel>.Fail("Failed to delete profile"));
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug($"[ProfilesController:DeleteProfiles] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{profileId}/tasks")]
        public async Task<IActionResult> GetTasksByProfileId(int profileId)
        {
            try
            {
                var result = await this.tasksService.RetriveTasksByProfileId(profileId);
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
                this.logger.LogDebug($"[ProfilesController:GetTasksByProfileId] Exception occurred: Inner exception: {ex.InnerException}");
                return BadRequest(ResponseWrapper<GetTasksViewModel>.Fail(ex.Message));
            }
        }
    }
}
