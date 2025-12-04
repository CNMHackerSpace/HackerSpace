// Copyright (c) CNM. All rights reserved.

using HackerSpace.Shared.Interfaces;
using HackerSpace.Components.Account;
using HackerSpace.Data;
using HackerSpace.Data.Mocks;
using HackerSpace.Data.Services;
using HackerSpace.Server.Components;
using HackerSpace.Server.Components;
using HackerSpace.Server.Data.Mocks;
using HackerSpace.Server.Data.Services;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HackerSpace
{
    /// <summary>
    /// Main entry point for the HackerSpace server application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application startup method.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            /*builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));*/

            // Inject the ContextFactory with connectionString
            builder.Services.AddDbContextFactory<ApplicationDbContext>(opt => opt.UseSqlite(connectionString));

            // Inject actual service instead of mock service
            builder.Services.AddScoped<IBadgesPageDataService, BadgeService>();


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            //Add data services
            builder.Services.AddSingleton<IBadgesPageDataService, BadgesPageServiceMock>();
            builder.Services.AddTransient<IEvaluatorsPageDataService, EvaluatorspageDataService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(HackerSpace.WebClient._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            RunMigrations(app);

            await AddRolesAsync(app);
            await AddAdminUser(app);

            app.Run();
        }

        private static async Task AddAdminUser(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string? email = app.Configuration.GetSection("Admin:Email").Value;
                string? password = app.Configuration.GetSection("Admin:Password").Value;
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && await userManager.FindByNameAsync(email) == null)
                {
                    var user = new ApplicationUser
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    var results = await userManager.CreateAsync(user, password);
                    if (results.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

        private static async Task AddRolesAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Instructor" };
                var roleExistenceTasks = roles.Select(role => roleManager.RoleExistsAsync(role)).ToArray();
                var existenceResults = await Task.WhenAll(roleExistenceTasks);
                var rolesToCreate = roles.Where((role, index) => !existenceResults[index]);
                foreach (var role in rolesToCreate)
                {
                    IdentityRole identityRole = new IdentityRole(role);
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }

        /// <summary>
        /// Runs database migrations at application startup.
        /// </summary>
        /// <param name="app">The web application instance.</param>
        private static void RunMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
