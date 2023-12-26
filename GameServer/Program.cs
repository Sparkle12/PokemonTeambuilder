using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Services;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settings = new Settings();
            builder.Configuration.Bind("Settings", settings);

            // Add services to the container.
            builder.Services.AddSingleton(settings);
            builder.Services.AddDbContext<GameDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")))  ;
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddScoped<PlayerService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
              
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}