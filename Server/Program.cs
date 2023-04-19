using Microsoft.AspNetCore.Authentication.JwtBearer; //For Auth0
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Server.Data;
using Server.Data.Interfaces;
using Server.Data.Mocks;
using Server.Data.Repositories;
using System.Configuration;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //For Auth0
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,c =>
            {
                c.Authority = builder.Configuration["Auth0:Authority"];
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = builder.Configuration["Auth0:Audience"],
                    ValidIssuer = builder.Configuration["Auth0:Domain"]
                };
            });

            //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = builder.Configuration["Auth0:Authority"];
            //    options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
            //    options.RequireHttpsMetadata = false;
            //});
            //End Auth0

            //Add data services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=Hackerspace.db"));

            builder.Services.AddTransient<IBadgesRepo, BadgesRepo>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            builder.Services.AddTransient<IUserRolesRepo, UserRolesRepo>();
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