using Client.Auth0;
using Client.Features.Auth0;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication; //Autho0
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //Add anonimous and secure clients so we can access both secured and anonimous endpoints
            builder.Services.AddHttpClient("AnonymousAPI", client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            });

            builder.Services.AddHttpClient("SecureAPIClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddTransient<CustomHttpClient>();
            //End add anonimous and secure clients

            builder.Services.AddScoped(sp => new HttpClient
            { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //For Auth0
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Auth0", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
            }).AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();
            //End Auth0

            await builder.Build().RunAsync();
        }
    }
}