using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace EventHorizon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await CreateDbIfNotInitialized(host);

            host.Run();
        }

        private static async Task CreateDbIfNotInitialized(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EventHorizonContext>();
                    var userManager = services.GetRequiredService<UserManager<Attendee>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var config = services.GetRequiredService<IConfiguration>();

                    await DbInitializer.InitializeDbAsync(context, userManager, roleManager, config);
                }
                catch (Exception ex)
                {
                    Log.Error("An error occured when initializing the DB: " + ex.Message);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration( //adds roles.json, used for initializing database with roles.
                    o => o.AddJsonFile("common.json", optional: false, reloadOnChange: true)
                );
    }
}
