using Data.Interfaces;
using Data.Mocks;
using HackerSpace.Server.Data;
using HackerSpace.Server.Data.Repositories;
using HackerSpace.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace HackerSpace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentityServer(options =>
            {
                // set path where to store keys
                //options.KeyManagement.KeyPath = "/home/shared/keys";

                // new key every 30 days
                options.KeyManagement.RotationInterval = TimeSpan.FromDays(30);

                // announce new key 2 days in advance in discovery
                options.KeyManagement.PropagationTime = TimeSpan.FromDays(2);

                // keep old key for 7 days in discovery for validation of tokens
                options.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);
            })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            //Add data services
            builder.Services.AddTransient<IBadgesRepo, BadgesRepo>();
            //End Add Data Services

            builder.Services.AddAuthentication()
                .AddIdentityServerJwt();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseDeveloperExceptionPage();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseIdentityServer();
            app.UseAuthorization();

            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }

            

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}