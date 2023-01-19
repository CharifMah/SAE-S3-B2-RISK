using Redis.OM;
using RISKAPI.HostedServices;
using RISKAPI.Hubs;
using System.Net;

namespace RISKAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            // Add services to the container.
            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));

            builder.Services.AddHostedService<IndexCreationService>();
            // Register the RedisCache service
            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = builder.Configuration["Redis"];
            //});

            //builder.Services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());

            builder.Services.AddSignalR();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseRouting();
            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyHub>("/JurrasicRisk/LobbyHub");
                endpoints.MapHub<PartieHub>("/JurrasicRisk/PartieHub");
            });

            app.Run();
        }
    }
}



