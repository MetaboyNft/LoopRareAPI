using LoopRareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace LoopRareAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClient(configuration));
            builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static CosmosDbService InitializeCosmosClient(IConfiguration configuration)
        {
            var client = new Microsoft.Azure.Cosmos.CosmosClient(configuration.GetValue<string>("CosmosDbEndpoint"), configuration.GetValue<string>("CosmosDbAuthKey"));
            var cosmosDbService = new CosmosDbService(client, configuration.GetValue<string>("CosmosDbDatabaseId"), configuration.GetValue<string>("CosmosDbContainerId"));
            return cosmosDbService;
        }
    }
}