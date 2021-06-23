using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EventHorizon.Data;
using Microsoft.AspNetCore.Identity;
using EventHorizon.Data.Entities;

namespace EventHorizon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EventHorizonContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EventHorizonContext")));

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<EventHorizonContext>(); //this dbContext will retrieve user and roleinfo.

            string adminPolicy = "AdminPolicy";
            string organizePolicy = "OrganizePolicy";

            services.AddAuthorization(o =>
            {
                o.AddPolicy(adminPolicy, p => p.RequireRole("Admin"));
                o.AddPolicy(organizePolicy, p => p.RequireRole("Admin", "Organizer"));

            });

            services.AddRazorPages(o =>
            {
                o.Conventions.AddPageRoute("/Index", "home"); //If user tyoe this, should get to index.
                o.Conventions.AddPageRoute("/Index", "EventHorizon");
                o.Conventions.AuthorizeFolder("/UserPages");//The whole folder requires authorization
                o.Conventions.AllowAnonymousToPage("/UserPages/Login");
                o.Conventions.AllowAnonymousToPage("/UserPages/Register");
                o.Conventions.AuthorizePage("/UserPages/UsersOverview", adminPolicy);
                o.Conventions.AuthorizePage("/UserPages/Edit", adminPolicy);

                o.Conventions.AuthorizePage("/EventPages/JoinEvent", adminPolicy);
            }).AddRazorRuntimeCompilation();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
