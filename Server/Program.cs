using Microsoft.AspNetCore.Authentication.JwtBearer; //For Auth0
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Interfaces;
using Server.Data.Repositories;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //For Auth0
            builder.Services.AddAuthentication(options =>      {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
                        }).AddJwtBearer(options =>                         
            {
                            options.Authority = builder
                .Configuration["Auth0:Authority"];           
                options.Audience = builder                     
                .Configuration["Auth0:ApiIdentifier"];       
            });
            //End Auth0

            //For access to Auth0 managment API
            builder.Services.AddAuth0AuthenticationClient(config =>
            {
                config.Domain = builder.Configuration["Auth0:Authority"];
                config.ClientId = builder.Configuration["Auth0:ClientId"];
                config.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
            });

            builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();
            //End access to Auth0 management API

            //Add data services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=Hackerspace.db"));
            builder.Services.AddTransient<IBadgesRepo, BadgesRepo>();
            //End Add Data Services

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            //Migrate and seed the database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
            //End migrate and seed database

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            //For Auth0
            app.UseAuthentication();
            app.UseAuthorization();
            //End Auth0

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}