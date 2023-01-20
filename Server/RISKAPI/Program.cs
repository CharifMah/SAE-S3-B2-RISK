using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Redis.OM;
using RISKAPI.Controllers;
using RISKAPI.HostedServices;
using RISKAPI.Hubs;
using System.Net.WebSockets;
using System.Text;

namespace RISKAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));

            builder.Services.AddHostedService<IndexCreationService>();

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

            //Map Hubs SignalR with endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyHub>("/JurrasicRisk/LobbyHub");
                endpoints.MapHub<PartieHub>("/JurrasicRisk/PartieHub");
            });

            app.Run();
        }       
    }
}



