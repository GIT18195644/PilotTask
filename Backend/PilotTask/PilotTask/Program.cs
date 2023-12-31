using Microsoft.EntityFrameworkCore;
using PilotTask.Configurations;
using PilotTask.Data.Infrastructure.Persistence.EFCore;
using Serilog;

// Setup logger for bootstrap process
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Setup logging for application
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    // Add services to the container.
    builder.Services.AddControllers();

    var allowedHosts = builder.Configuration.GetValue(typeof(string), "AllowedHosts") as string;

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                if (allowedHosts == null || allowedHosts == "*")
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    return;
                }
                string[] hosts;
                if (allowedHosts.Contains(';'))
                    hosts = allowedHosts.Split(';');
                else
                {
                    hosts = new string[1];
                    hosts[0] = allowedHosts;
                }
                builder.WithOrigins(hosts)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddPilotTaskServices(builder.Configuration, builder.Environment);

    builder.Services.AddMassTransitComponents(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //Configure cors
    app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());

    app.UseForwardedHeaders();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseRouting();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
