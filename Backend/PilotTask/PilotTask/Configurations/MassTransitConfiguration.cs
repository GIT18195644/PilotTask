using MassTransit;
using PilotTask.Data.Application.Commands.Profiles.CreateProfiles;
using PilotTask.Data.Application.Commands.Profiles.DeleteProfiles;
using PilotTask.Data.Application.Commands.Profiles.UpdateProfiles;
using PilotTask.Data.Application.Queries.Profiles.GetProfiles;
using PilotTask.Data.Application.Queries.Profiles.GetProfilesByProfileId;
using PilotTask.Data.Application.Queries.Tasks.GetTasksByProfileId;
using PilotTask.Data.Entities;
using PilotTask.Extensions;
using PilotTask.Models;

namespace PilotTask.Configurations
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator(x =>
            {
                // commands
                x.AddConsumer<CreateProfiles>();
                x.AddConsumer<UpdateProfiles>();
                x.AddConsumer<DeleteProfiles>();

                // queries
                x.AddConsumer<GetProfiles>();
                x.AddConsumer<GetProfilesByProfileId>();
                x.AddConsumer<GetTasksByProfileId>();

                x.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
            });

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter(); // set endpoint names using convention
                MessageDataDefaults.TimeToLive = TimeSpan.FromHours(1);

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rmq = configuration.GetSection("RabbitMQ").Get<RabbitMQModel>();
                    cfg.Host(rmq?.Host, rmq.Port, rmq?.VirtualHost, h =>
                    {
                        h.Username(rmq?.Username);
                        h.Password(rmq?.Password);
                    });
                    cfg.ConfigureEndpoints(context);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
