using FlightPlanner.Core.Logic;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using FlightPlannerBackend.Handlers;
using FlightPlannerBackend.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<FlightPlannerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IDbService, DbService>();
            builder.Services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
            builder.Services.AddTransient<IEntityService<Flight>, EntityService<Flight>>();
            builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}