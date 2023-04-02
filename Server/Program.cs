using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

            // Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddMicrosoftIdentityWebApi(options =>
            //        {
            //            builder.Configuration.Bind("AzureAdB2C", options);

            //            options.TokenValidationParameters.NameClaimType = "Name";
            //            options.TokenValidationParameters.NameClaimType = "Email";
            //        },
            //options => { builder.Configuration.Bind("AzureAdB2C", options); });
            // End of the Microsoft Identity platform block  
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=Hackerspace.db"));
            
            //Add data services
            builder.Services.AddTransient<IBadgesRepo, BadgesRepo>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            //End Add Data Services

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            //Update the database from migrations
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}