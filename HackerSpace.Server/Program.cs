// Copyright (c) 2025. All rights reserved.

using HackerSpace.Components.Account;
using HackerSpace.Data;
using HackerSpace.Data.Services;
using HackerSpace.Server.Components;
using HackerSpace.Server.Data.Services;
using HackerSpace.Server.Services;
using HackerSpace.Shared.Interfaces;
using HackerSpace.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
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

            // Force-load appsettings.Production.json if present (overrides appsettings.json)
            builder.Configuration.AddJsonFile(
                "appsettings.Production.json",
                optional: true,
                reloadOnChange: true);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddCircuitOptions(o => o.DetailedErrors = true)
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

            // Inject the ContextFactory with connectionString
            builder.Services.AddDbContextFactory<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            // For script outlet component
            builder.Services.AddSingleton<ScriptRegistry>();

            // Add data services
            builder.Services.AddScoped<IBadgesPageDataService, BadgesPageDataService>();
            builder.Services.AddTransient<IEvaluatorsPageDataService, EvaluatorspageDataService>();
            builder.Services.AddTransient<IBadgeEditPageDataService, BadgesEditPageDataService>();
            builder.Services.AddScoped<IBadgeViewPageDataService, BadgeViewPageDataService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

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

            // ... in Program.cs after builder.Build()
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".data"] = "application/octet-stream";
            provider.Mappings[".wasm"] = "application/wasm"; // or application/octet-stream
            provider.Mappings[".symbols.json"] = "application/json";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider,
                
                // Handle gzipped files for unity game
                OnPrepareResponse = ctx => 
                {
                    var path = ctx.File.PhysicalPath ?? "";
                    if (path.EndsWith(".gz"))
                    {
                        ctx.Context.Response.Headers["Content-Encoding"] = "gzip";

                        // Fix content-type based on the *original* extension
                        if (path.EndsWith(".js.gz")) ctx.Context.Response.ContentType = "application/javascript";
                        if (path.EndsWith(".data.gz")) ctx.Context.Response.ContentType = "application/octet-stream";
                        if (path.EndsWith(".wasm.gz")) ctx.Context.Response.ContentType = "application/wasm";
                    }
                }
            });


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

        /// <summary>
        /// Adds an admin user to the identity system at application startup if the admin credentials are provided in configuration.
        /// </summary>
        /// <param name="app">The web application instance.</param>
        private static async Task AddAdminUser(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string? email = app.Configuration.GetSection("Admin:Email").Value;
                string? password = app.Configuration.GetSection("Admin:Password").Value;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    // Nothing to do when admin credentials are not provided.
                    return;
                }

                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                var errors = new System.Collections.Generic.List<string>();

                bool isValidPassword = await ValidatePassword(userManager, password, user, errors);

                if (!isValidPassword)
                {
                    Console.Error.WriteLine($"Admin password does not meet complexity requirements: {string.Join(" ", errors)}");
                    return;
                }

                if (await userManager.FindByNameAsync(email) == null)
                {
                    var results = await userManager.CreateAsync(user, password);
                    if (results.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to create admin user: {string.Join(", ", results.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        /// <summary>
        /// Validates the specified password for the given user using the configured password validators.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="password">The password to validate.</param>
        /// <param name="user">The user for whom the password is being validated.</param>
        /// <param name="errors">A list to which any validation error messages will be added.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains <c>true</c> if the password is valid; otherwise, <c>false</c>.
        /// </returns>
        private static async Task<bool> ValidatePassword(UserManager<ApplicationUser> userManager, string password, ApplicationUser user, List<string> errors)
        {
            bool isValidPassword = true;

            // Validate password using configured Identity password validators
            var validators = userManager.PasswordValidators;
            if (validators != null && validators.Count > 0)
            {
                foreach (var validator in validators)
                {
                    var validationResult = await validator.ValidateAsync(userManager, user, password);
                    if (!validationResult.Succeeded)
                    {
                        errors.Concat(validationResult.Errors.Select(e => e.Description).ToList());
                        isValidPassword = false;
                    }
                }
            }
            else
            {
                // Fallback basic checks in case no validators are configured
                if (password.Length < 6) errors.Add("Password must be at least 6 characters.");
                if (!password.Any(char.IsUpper)) errors.Add("Password must contain an uppercase letter.");
                if (!password.Any(char.IsLower)) errors.Add("Password must contain a lowercase letter.");
                if (!password.Any(char.IsDigit)) errors.Add("Password must contain a digit.");
                if (!password.Any(ch => !char.IsLetterOrDigit(ch))) errors.Add("Password must contain a non-alphanumeric character.");

                if (errors.Count > 0)
                {

                    isValidPassword = false;
                }
            }

            return isValidPassword;
        }

        /// <summary>
        /// Adds required roles to the identity system at application startup if they do not already exist.
        /// </summary>
        /// <param name="app">The web application instance.</param>
        private static async Task AddRolesAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Instructor" };
                foreach (var role in roles)
                {
                    if (await roleManager.RoleExistsAsync(role))
                    {
                        continue;
                    }
                    IdentityRole identityRole = new IdentityRole(role);
                    var result = await roleManager.CreateAsync(identityRole);
                    if (!result.Succeeded)
                    {
                        Console.Error.WriteLine($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
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
