using Microsoft.EntityFrameworkCore;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.IProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IRepositories.ITasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.IProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService;
using PilotTask.Data.Infrastructure.Persistence.Data.Repositories.ProfilesRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Repositories.TasksRepository;
using PilotTask.Data.Infrastructure.Persistence.Data.Services.ProfilesService;
using PilotTask.Data.Infrastructure.Persistence.Data.Services.TasksService;
using PilotTask.Data.Infrastructure.Persistence.EFCore;

namespace PilotTask.Configurations
{
    public static class PilotTaskConfigurations
    {
        public static void AddPilotTaskServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsProduction())
            {
                services.AddDbContext<PilotTaskDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration["ConnectionStrings:ProdConnection"]);
                });
            }
            else
            {
                services.AddDbContext<PilotTaskDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration["ConnectionStrings:DevConnection"]);
                });
            }

            services.AddScoped<IProfilesReadRepository, ProfilesReadRepository>();
            services.AddScoped<IProfilesWriteRepository, ProfilesWriteRepository>();
            services.AddScoped<ITasksReadRepository, TasksReadRepository>();
            services.AddScoped<ITasksWriteRepository, TasksWriteRepository>();

            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddScoped<ITasksService, TasksService>();
        }
    }
}
